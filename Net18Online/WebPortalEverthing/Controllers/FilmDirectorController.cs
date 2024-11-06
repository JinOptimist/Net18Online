using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.MoviePoster;
using WebPortalEverthing.Models.FilmDirector;

namespace WebPortalEverthing.Controllers
{
    public class FilmDirectorController : Controller
    {
        private IFilmDirectorRepositoryReal _filmDirectorRepositoryReal;
        private IMoviePosterRepositoryReal _moviePosterRepositoryReal;

        public FilmDirectorController(IFilmDirectorRepositoryReal filmDirectorRepositoryReal, IMoviePosterRepositoryReal moviePosterRepositoryReal)
        {
            _filmDirectorRepositoryReal = filmDirectorRepositoryReal;
            _moviePosterRepositoryReal = moviePosterRepositoryReal;
        }
        public IActionResult Index()
        {
            var filmDirectorViewModels = _filmDirectorRepositoryReal
                .GetAllWithFilms()
                .Select(x => new FilmDirectorShortInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Movies = x.Films.Select(movieData =>
                        new MovieNameAndIdViewModel
                        {
                            Id = movieData.Id,
                            Name = movieData.Name,
                        }).ToList()
                })
                .ToList();

            var movieViewModels = _moviePosterRepositoryReal
                .GetWithoutFilmDirector()
                .Select(x => new MovieNameAndIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            var viewModel = new IndexFilmDirectorViewModel
            {
                FilmDirectors = filmDirectorViewModels,
                Movies = movieViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateFilmDirectorViewModel viewModel)
        {
            var filmDirector = new FilmDirectorData
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName
            };

            _filmDirectorRepositoryReal.Add(filmDirector);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LinkMovieAndFilmDirector(int filmDirectorId, int movieId)
        {
            _filmDirectorRepositoryReal.LinkMovie(filmDirectorId, movieId);
            return RedirectToAction("Index");
        }
    }
}

