using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models;
using WebPortalEverthing.Models.MoviePoster;

namespace WebPortalEverthing.Controllers
{
    public class MoviePosterController : Controller
    {
        // BAD. DO NOT USE THIS ON PROD
        public static List<PosterViewModel> posterViewModels = new List<PosterViewModel>();
        public static List<PosterViewModel> newPosterInListOfPostersViewModels = new List<PosterViewModel>();

        public IActionResult Index(string name, int age)
        {
            var model = new MoviePosterIndexViewModel();
            model.Hours = DateTime.Now.Hour;
            model.Minutes = DateTime.Now.Minute;
            model.Seconds = DateTime.Now.Second;
            return View(model);
        }

        public IActionResult AllPosters(int? count)
        {
            posterViewModels.Clear();
            if (!posterViewModels.Any())
            {
                for (int i = 0; i < (count ?? 4); i++)
                {
                    var posterNumber = (i % 4) + 1;
                    var viewModel = new PosterViewModel
                    {
                        Name = $"Poster {posterNumber}",
                        ImageSrc = $"/images/MoviePoster/Poster{posterNumber}.jpg",
                        Tags = new List<string> { "action", "drama" }
                    };
                    posterViewModels.Add(viewModel);
                }
            }
            posterViewModels.AddRange(newPosterInListOfPostersViewModels);
            return View(posterViewModels);
        }

        [HttpGet]
        public IActionResult CreatePoster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoster(PosterCreationViewModel viewModel)
        {
            var poster = new PosterViewModel
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = viewModel.Tags,
            };
            newPosterInListOfPostersViewModels.Add(poster);

            return RedirectToAction("AllPosters");
        }

    }
}
