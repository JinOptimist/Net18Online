using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;
using WebPortalEverthing.Models.AnimeReview;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class AnimeReviewController : Controller
    {
        private IAnimeReviewRepositoryReal _animeReviewRepository;
        private IAnimeCatalogRepositoryReal _animeCatalogRepository;
        private AuthService _authService;
        public AnimeReviewController(IAnimeReviewRepositoryReal animeReviewRepositoryReal, IAnimeCatalogRepositoryReal animeCatalogRepositoryReal, AuthService authService)
        {
            _animeReviewRepository = animeReviewRepositoryReal;
            _animeCatalogRepository = animeCatalogRepositoryReal;
            _authService = authService;
        }

        public IActionResult Index()
        {
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
                Index = new List<AnimeReviewViewModel>()
            };

            foreach (var item in animeCataolgViewModels)
            {
                viewModel.Index.Add(new AnimeReviewViewModel
                {
                    Anime = item,
                    Reviews = _animeReviewRepository
                        .GetAllWithAnime(item.Id)
                        .Select(x => new AnimeReviewShortInfoViewModel
                        {
                            Id = x.Id,
                            Review = x.Review,
                            UserName = _animeReviewRepository.GetUserName(x.Id),
                            Anime = new AnimeCatalogNameAndIdViewModel
                            {
                                Id = x.Id,
                            }
                        })
                        .ToList()
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateReviewAndLinktoAnime(IndexAnimeReviewViewModel viewModel, int animeId)
        {
            var currentUserId = _authService.GetUserId();

            if(currentUserId == null)
            {
                return RedirectToAction("Register", "Auth");
            }
            var animeReview = new AnimeReviewData
            {
                Review = viewModel.NewReview.Review
            };

            _animeReviewRepository.Create(animeReview, currentUserId!.Value);
            _animeReviewRepository.LinkAnime(animeReview.Id, animeId);

            return RedirectToAction("Index");
        }
    }
}
