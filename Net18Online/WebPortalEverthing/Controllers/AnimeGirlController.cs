using Enums;
using Enums.Girls;
using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models;
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
        private GeneratorAnimeGirls _generatorAnimeGirls;
        private IWebHostEnvironment _webHostEnvironment;
        private AutoMapperSmile _autoMapperSmile;
        private int DEFUALT_PAGE = 1;
        private int DEFUALT_PER_PAGE = 5;

        public AnimeGirlController(IAnimeGirlRepositoryReal animeGirlRepository,
            IUserRepositryReal userRepositryReal,
            AuthService authService,
            IMangaRepositoryReal mangaRepositoryReal,
            IWebHostEnvironment webHostEnvironment,
            GeneratorAnimeGirls generatorAnimeGirls,
            AutoMapperSmile autoMapperSmile)
        {
            _animeGirlRepository = animeGirlRepository;
            _userRepositryReal = userRepositryReal;
            _authService = authService;
            _mangaRepositoryReal = mangaRepositoryReal;
            _webHostEnvironment = webHostEnvironment;
            _generatorAnimeGirls = generatorAnimeGirls;
            _autoMapperSmile = autoMapperSmile;
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

        [IsAuthenticated]
        public IActionResult AllGirls(int? page, int? perPage, 
            GirlSortType? orderField, OrderDirection? orderDirection)
        {
            page = page ?? DEFUALT_PAGE;
            perPage = perPage ?? DEFUALT_PER_PAGE;
            orderField = orderField ?? GirlSortType.Default;
            orderDirection = orderDirection ?? OrderDirection.Asc;

            if (!_animeGirlRepository.Any())
            {
                GenerateDefaultAnimeGirl();
            }

            var currentUserId = _authService.GetUserId()!;

            var user = _userRepositryReal.Get(currentUserId.Value)!;

            var girlsFromDb = _animeGirlRepository
                .GetAllWithCreatorsAndManga(
                page.Value, 
                perPage.Value, 
                orderField.Value, 
                orderDirection.Value);

            var girlsViewModels = girlsFromDb
                .Items
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
                            || dbGirl.Creator?.Id == currentUserId,
                        LikeCount = dbGirl.UsersWhoLikeIt.Count(),
                        IsLiked = dbGirl.UsersWhoLikeIt.Any(x => x.Id == user.Id)
                    }
                )
                .ToList();

            var pagginatorViewModel = new PagginatorViewModel<GirlViewModel>()
            {
                Items = girlsViewModels,
                Page = page.Value,
                PerPage = perPage.Value,
                TotalRecords = girlsFromDb.TotalRecords
            };

            var viewModel = new AllGirlsViewModel
            {
                Girls = pagginatorViewModel,
                Mangas = _mangaRepositoryReal
                    .GetAll()
                    .Select(x => new MangaNameAndIdViewModel
                    {
                        Id = x.Id,
                        Title = x.Title
                    })
                    .ToList()
            };

            return View(viewModel);
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

        public IActionResult UpdateImage(int id, string url)
        {
            _animeGirlRepository.UpdateImage(id, url);
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
                .Select(_autoMapperSmile.Map<DuplicateInfoViewModel, GirlsWithDuplicateInfo>)
                .ToList();

            return View(viewModels);
        }
    }
}
