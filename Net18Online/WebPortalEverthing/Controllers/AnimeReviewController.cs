using Everything.Data.Fake.Models;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;
using WebPortalEverthing.Models.AnimeReview;

namespace WebPortalEverthing.Controllers
{
    public class AnimeReviewController : Controller
    {
        private IAnimeReviewRepositoryReal _animeReviewRepository;
        private IAnimeCatalogRepositoryReal _animeCatalogRepository;

        public AnimeReviewController(IAnimeReviewRepositoryReal animeReviewRepositoryReal, IAnimeCatalogRepositoryReal animeCatalogRepositoryReal)
        {
            _animeReviewRepository = animeReviewRepositoryReal;
            _animeCatalogRepository = animeCatalogRepositoryReal;
        }

        public IActionResult Index()
        {
            var animeReviewViewModels = _animeReviewRepository
                .GetAll()
                .Select(x => new AnimeReviewShortInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Review = x.Review,
                    Anime = new AnimeCatalogNameAndIdViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }
                })
                .ToList();

            var animeCataolgViewModels = _animeCatalogRepository
                .GetAll()
                .Select(x => new AnimeCatalogNameAndIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgSrc = x.ImageSrc
                })
                .ToList();

            var viewModel = new IndexAnimeReviewViewModel
            {
                Reviews = animeReviewViewModels,
                Animes = animeCataolgViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnimeReviewViewModel viewModel)
        {
            var animeReview = new AnimeReviewData
            {
                Name = viewModel.Name,
                Review = viewModel.Review
            };

            _animeReviewRepository.Add(animeReview);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LinkAnimeAndReview(int animeId, int animeReviewId)
        {
            _animeReviewRepository.LinkAnime(animeReviewId, animeId);
            return RedirectToAction("Index");
        }
    }
}
