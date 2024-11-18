using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.MoviePoster;

namespace WebPortalEverthing.Controllers
{
    public class MoviePosterController : Controller
    {
        private IMoviePosterRepositoryReal _moviePosterRepository;
        private WebDbContext _webDbContext;
        public MoviePosterController(IMoviePosterRepositoryReal moviePosterRepository, WebDbContext webDbContext)
        {
            _moviePosterRepository = moviePosterRepository;
            _webDbContext = webDbContext;
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
            var countElementInDb = _moviePosterRepository.GetAll().ToList();
            if (!_moviePosterRepository.Any() || countElementInDb.Count < count)
            {
                GenerateDefaultMoviePoster(count - countElementInDb.Count);
            }

            var moviesFromDb = _moviePosterRepository.GetAllInCount(count ?? countElementInDb.Count);

            var movieViewModels = moviesFromDb
                //.Take(count ?? countElementInDb.Count)
                .Select(dbMovie =>
                    new MovieViewModel
                    {
                        Id = dbMovie.Id,
                        Name = dbMovie.Name,
                        ImageSrc = dbMovie.ImageSrc,
                        Tags = new List<string>() //dbMovie.Tags
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
                    //Tags = new List<string> { "action", "drama" }
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
            //if (_moviePosterRepository.HasSimilarUrl(viewModel.Url))
            //{
            //    ModelState.AddModelError(
            //        nameof(MovieCreationViewModel.Url),
            //        "Такой url уже есть");
            //}

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dataMovie = new MovieData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                //Tags = viewModel.Tags,
            };
            //_moviePosterRepository.Add(dataMovie);

            _webDbContext.Movies.Add(dataMovie);
            _webDbContext.SaveChanges();

            return RedirectToAction("AllPosters");
        }

        public IActionResult UpdateName(string newName, int id)
        {
            _moviePosterRepository.UpdateName(id, newName);
            return RedirectToAction("AllPosters");
        }

        public IActionResult UpdateImage(int id, string url)
        {
            _moviePosterRepository.UpdateImage(id, url);
            return RedirectToAction("AllPosters");
        }

        public IActionResult Remove(int id)
        {
            _moviePosterRepository.Delete(id);
            return RedirectToAction("AllPosters");
        }
    }
}
