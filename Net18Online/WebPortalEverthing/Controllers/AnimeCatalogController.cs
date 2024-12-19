using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.AnimeCatalog;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        private IAnimeCatalogRepositoryReal _animeCatalogRepository;
        private WebDbContext _webDbContext;
        private IWebHostEnvironment _webHostEnvironment;
        private AuthService _authService;

        public AnimeCatalogController(IAnimeCatalogRepositoryReal animeCatalogRepository, WebDbContext webDbContext, IWebHostEnvironment webHostEnvironment, AuthService authService)
        {
            _animeCatalogRepository = animeCatalogRepository;
            _webDbContext = webDbContext;
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public IActionResult Index(int? count)
        {
            if (!_animeCatalogRepository.Any())
            {
                CreateDefoltAnimeCatalog(count);
            }

            var animesFromDb = _animeCatalogRepository.GetAll();

            var animesViewModels = animesFromDb
                .Select(dbAnime =>
                    new AnimeCatalogViewModel
                    {
                        Id = dbAnime.Id,
                        Name = dbAnime.Name,
                        ImageSrc = dbAnime.ImageSrc,
                    }
                )
                .ToList();

            return View(animesViewModels);
        }

        private void CreateDefoltAnimeCatalog(int? count)
        {
            for (int i = 0; i < (count ?? 4); i++)
            {
                var animeNumber = (i % 4) + 1;
                var dataModel = new AnimeData
                {
                    Name = $"Anime {animeNumber}",
                    ImageSrc = $"/images/AnimeCatalog/Anime{animeNumber}.jpg",
                };

                _animeCatalogRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [IsAuthenticated]
        [HttpPost]
        public IActionResult UpdateAvatar(IFormFile anime, AnimeCatalogCreationViewModel viewModel)
        {
            if (viewModel.Name == null)
            {
                throw new Exception();
            }
            if (anime == null && viewModel.ImageSrc == null)
            {
                throw new Exception();
            }
            if (viewModel.ImageSrc != null)
            {
                var animeData = new AnimeData
                {
                    Name = viewModel.Name,
                    ImageSrc = viewModel.ImageSrc,
                };

                _animeCatalogRepository.Add(animeData);
                return RedirectToAction("Index");
            }

            var webRootPath = _webHostEnvironment.WebRootPath;

            var animeFileName = $"Anime-{anime!.FileName.GetHashCode()}.jpg";

            var path = Path.Combine(webRootPath, "images", "AnimeCatalog", animeFileName);

            if (Path.Exists(path))
            {
                return RedirectToAction("Index");
            }

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                anime
                    .CopyToAsync(fileStream)
                    .Wait();
            }

            var animeUrl = $"/images/AnimeCatalog/Anime-{anime!.FileName.GetHashCode()}.jpg";

            var dataCatalog = new AnimeData
            {
                Name = viewModel.Name,
                ImageSrc = animeUrl,
            };

            _animeCatalogRepository.Add(dataCatalog);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetShortestReview()
        {
            var shortestReviews = _animeCatalogRepository.GetAnimeWithShortestReview();
            var viewModel = shortestReviews.Select(x => new AnimeWithShortestReviewViewModel
            {
                AnimeId = x.AnimeId,
                ImageSrc = x.ImageSrc,
                AnimeName = x.AnimeName,
                ShortestReviewLength = x.ShortestReviewLength
            })
                .ToList();
            return View(viewModel);
        }
    }
}
