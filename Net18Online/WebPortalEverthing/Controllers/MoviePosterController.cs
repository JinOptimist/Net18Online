using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.MoviePoster;

namespace WebPortalEverthing.Controllers
{
    public class MoviePosterController : Controller
    {
        private IMoviePosterRepository _moviePosterRepository;

        public MoviePosterController(IMoviePosterRepository moviePosterRepository)
        {
            _moviePosterRepository = moviePosterRepository;
        }

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
            var countElementInDb = _moviePosterRepository.GetAll();
            if (!_moviePosterRepository.Any() || countElementInDb.Count < count)
            {
                GenerateDefaultMoviePoster(count - countElementInDb.Count);
            }

            var moviesFromDb = _moviePosterRepository.GetAllInCount(count ?? countElementInDb.Count);

            var movieViewModels = moviesFromDb
                .Take(count ?? countElementInDb.Count)
                .Select(dbMovie =>
                    new MovieViewModel
                    {
                        Id = dbMovie.Id,
                        Name = dbMovie.Name,
                        ImageSrc = dbMovie.ImageSrc,
                        Tags = dbMovie.Tags
                    }
                )
                .ToList();

            return View(movieViewModels);
        }

        private void GenerateDefaultMoviePoster(int? count)
        {
            for (int i = 0; i < (count ?? 4); i++)
            {
                var movieNumber = (i % 4) + 1;
                var dataModel = new MovieData
                {
                    Name = $"Poster {movieNumber}",
                    ImageSrc = $"/images/MoviePoster/Poster{movieNumber}.jpg",
                    Tags = new List<string> { "action", "drama" }
                };
                _moviePosterRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult CreatePoster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoster(MovieCreationViewModel viewModel)
        {
            var dataMovie = new MovieData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = viewModel.Tags,
            };
            _moviePosterRepository.Add(dataMovie);

            return RedirectToAction("AllPosters");
        }

    }
}
