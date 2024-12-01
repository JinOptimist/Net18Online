﻿using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.AnimeGirl.Profile;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class AnimeGirlController : Controller
    {
        private int DEFAULT_GIRL_COUNT = 4;
        private IAnimeGirlRepositoryReal _animeGirlRepository;
        private IMangaRepositoryReal _mangaRepositoryReal;
        private IUserRepositryReal _userRepositryReal;
        private AuthService _authService;
        private WebDbContext _webDbContext;
        private IWebHostEnvironment _webHostEnvironment;

        public AnimeGirlController(IAnimeGirlRepositoryReal animeGirlRepository,
            WebDbContext webDbContext,
            IUserRepositryReal userRepositryReal,
            AuthService authService,
            IMangaRepositoryReal mangaRepositoryReal,
            IWebHostEnvironment webHostEnvironment)
        {
            _animeGirlRepository = animeGirlRepository;
            _webDbContext = webDbContext;
            _userRepositryReal = userRepositryReal;
            _authService = authService;
            _mangaRepositoryReal = mangaRepositoryReal;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string name, int age)
        {
            var model = new AnimeGirlIndexViewModel();
            model.Name = name ?? "Ivan";
            model.Age = age;
            model.Hours = DateTime.Now.Hour;
            model.Minutes = DateTime.Now.Minute;
            model.Seconds = DateTime.Now.Second;
            return View(model);
        }

        public IActionResult AllGirls()
        {
            if (!_animeGirlRepository.Any())
            {
                GenerateDefaultAnimeGirl();
            }

            var currentUserId = _authService.GetUserId();
            if (currentUserId is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _userRepositryReal.Get(currentUserId.Value);

            var girlsFromDb = _animeGirlRepository.GetAllWithCreatorsAndManga();

            var girlsViewModels = girlsFromDb
                .Select(dbGirl =>
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        ImageSrc = dbGirl.ImageSrc,
                        Tags = new List<string>(),
                        CreatorName = dbGirl.Creator?.Login ?? "Неизвестный",
                        MangaName = dbGirl.Manga?.Title ?? "Из фанфика",
                        CanDelete = dbGirl.Creator is null
                            || dbGirl.Creator?.Id == currentUserId
                    }
                )
                .ToList();

            return View(girlsViewModels);
        }

        private void GenerateDefaultAnimeGirl()
        {
            for (int i = 0; i < DEFAULT_GIRL_COUNT; i++)
            {
                var girlNumber = (i % 4) + 1;
                var dataModel = new GirlData
                {
                    Name = $"Girl {girlNumber}",
                    ImageSrc = $"/images/AnimeGirl/Girl{girlNumber}.jpg",
                    // Tags = new List<string> { "4 size", "red" }
                };

                _animeGirlRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GirlCreationViewModel();

            viewModel.Mangas = _mangaRepositoryReal
                .GetAll()
                .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(GirlCreationViewModel viewModel)
        {
            if (_animeGirlRepository.HasSimilarName(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(GirlCreationViewModel.Name),
                    "Слишком похожее имя");
            }

            if (!ModelState.IsValid)
            {
                viewModel.Mangas = _mangaRepositoryReal
                    .GetAll()
                    .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                    .ToList();

                return View(viewModel);
            }

            var currentUserId = _authService.GetUserId();

            var dataGirl = new GirlData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
            };

            _animeGirlRepository.Create(dataGirl, currentUserId!.Value, viewModel.MangaId);

            return RedirectToAction("AllGirls");
        }

        public IActionResult UpdateName(string newName, int id)
        {
            _animeGirlRepository.UpdateName(id, newName);
            return RedirectToAction("AllGirls");
        }

        public IActionResult UpdateImage(int id, string url)
        {
            _animeGirlRepository.UpdateImage(id, url);
            return RedirectToAction("AllGirls");
        }

        public IActionResult Remove(int id)
        {
            _animeGirlRepository.Delete(id);
            return RedirectToAction("AllGirls");
        }

        [IsAuthenticated]
        public IActionResult Profile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.UserName = _authService.GetName()!;

            var userId = _authService.GetUserId()!.Value;

            viewModel.AvatarUrl = _userRepositryReal.GetAvatarUrl(userId);

            viewModel.Mangas = _mangaRepositoryReal
                .GetMangaWithInfoAboutAuthors(userId)
                .Select(x => new MangaShortInfoViewModel
                {
                    Name = x.Name,
                    IsCreatedWithСharacter = x.HasCharaterWithSpecialAuthor
                })
                .ToList();

            viewModel.Girls = _animeGirlRepository
                .GetAllByAuthorId(userId)
                .Select(x => new GirlShortInfoViewModel
                {
                    Name = x.Name,
                    Url = x.ImageSrc
                })
                .ToList();

            return View(viewModel);
        }

        [IsAuthenticated]
        [HttpPost]
        public IActionResult UpdateAvatar(IFormFile avatar)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var userId = _authService.GetUserId()!.Value;
            var avatarFileName = $"avatar-{userId}.jpg";

            var path = Path.Combine(webRootPath, "images", "avatars", avatarFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                avatar
                    .CopyToAsync(fileStream)
                    .Wait();
            }

            var avatarUrl = $"/images/avatars/{avatarFileName}";
            _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

            return RedirectToAction("Profile");
        }

        public IActionResult DuplicateInfo()
        {
            var viewModels = _animeGirlRepository
                .GetGirlsWithDuplicateInfo()
                .Select(db => new DuplicateInfoViewModel
                {
                    Id = db.Id,
                    Name = db.Name,
                    ImageSrc = db.ImageSrc,
                    DuplicateStatus = db.DuplicateStatus,
                    OriginId = db.OriginId,
                    OriginName = db.OriginName,
                    UniqStatus = db.UniqStatus
                })
                .ToList();

            return View(viewModels);
        }
    }
}
