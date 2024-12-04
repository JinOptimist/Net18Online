using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.MoviePoster;
using WebPortalEverthing.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.MoviePoster.Profile;

namespace WebPortalEverthing.Controllers
{
    public class MoviePosterController : Controller
    {
        private IMoviePosterRepositoryReal _moviePosterRepository;
        private IFilmDirectorRepositoryReal _filmDirectorRepository;
        private IUserRepositryReal _userRepositryReal;
        private WebDbContext _webDbContext;
        private AuthService _authService;
        public MoviePosterController(IMoviePosterRepositoryReal moviePosterRepository,
            WebDbContext webDbContext,
            IUserRepositryReal userRepositryReal,
            AuthService authService,
            IFilmDirectorRepositoryReal filmDirectorRepository)
        {
            _moviePosterRepository = moviePosterRepository;
            _webDbContext = webDbContext;
            _userRepositryReal = userRepositryReal;
            _authService = authService;
            _filmDirectorRepository = filmDirectorRepository;
        }

        public IActionResult Index(string name, int age)
        {
            var model = new MoviePosterIndexViewModel();
            model.Hours = DateTime.Now.Hour;
            model.Minutes = DateTime.Now.Minute;
            model.Seconds = DateTime.Now.Second;

            var userName = _authService.GetName();
            model.UserName = userName;

            return View(model);
        }
        
        public IActionResult AllPosters(int? count)
        {
            var countElementInDb = _moviePosterRepository.GetAll().ToList();
            if (!_moviePosterRepository.Any() || countElementInDb.Count < count)
            {
                GenerateDefaultMoviePoster(count - countElementInDb.Count);
            }

            var currentUserId = _authService.GetUserId();
            if (currentUserId is null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _userRepositryReal.Get(currentUserId.Value);

            var moviesFromDb = _moviePosterRepository.GetAllWithСreatorsAndFilmDirectors();

            var movieViewModels = moviesFromDb
                //.Take(count ?? countElementInDb.Count)
                .Select(dbMovie =>
                    new MovieViewModel
                    {
                        Id = dbMovie.Id,
                        Name = dbMovie.Name,
                        ImageSrc = dbMovie.ImageSrc,
                        Tags = new List<string>(), //dbMovie.Tags
                        CreatorName = dbMovie.Creator?.Login ?? "Неизвестный автор",
                        FilmDirector = dbMovie.FilmDirector?.LastName ?? "Неизвестный режиссер",
                        CanDelete = dbMovie.Creator is null 
                            || dbMovie.Creator?.Id == currentUserId 
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
            var viewModel = new MovieCreationViewModel();
            
            viewModel.FilmDirectors = _filmDirectorRepository
                .GetAll()
                .Select(x => new SelectListItem(x.LastName, x.Id.ToString())) // .Select(x => new SelectListItem(x.Name + " " + x.LastName, x.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreatePoster(MovieCreationViewModel viewModel)
        {
            if (_moviePosterRepository.HasSimilarUrl(viewModel.Url))
            {
                ModelState.AddModelError(
                    nameof(MovieCreationViewModel.Url),
                    "Такой url уже есть");
            }

            if (!ModelState.IsValid)
            {
                viewModel.FilmDirectors = _filmDirectorRepository
                    .GetAll()
                    .Select(x => new SelectListItem(x.LastName, x.Id.ToString())) // .Select(x => new SelectListItem(x.Name + " " + x.LastName, x.Id.ToString()))
                    .ToList();

                return View(viewModel);
            }

            var currentUserId = _authService.GetUserId();

            var dataMovie = new MovieData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                //Tags = viewModel.Tags,
            };
            //_moviePosterRepository.Add(dataMovie);

            _moviePosterRepository.Create(dataMovie, currentUserId!.Value, viewModel.FilmDirectorId);

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

        public IActionResult Profile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.UserName = _authService.GetName()!;

            var userId = _authService.GetUserId()!.Value;

            viewModel.FilmDirectors = _filmDirectorRepository
                .GetFilmDirectorWithInfoAboutCreator(userId)
                .Select(x => new FilmDirectorShortInfoViewModel
                {
                    Name = x.Name,
                    IsCreatedWithMovies = x.HasMoviesWithSpecialCreator
                })
                .ToList();

            viewModel.Movies = _moviePosterRepository
                .GetAllByCreatorId(userId)
                .Select(x => new MovieShortInfoViewModel
                {
                    Name = x.Name,
                    Url = x.ImageSrc
                })
                .ToList();

            return View(viewModel);
        }

        public IActionResult ShowCountWithFirstAndLastElementInDb()
        {
            var viewModels = _moviePosterRepository
                .GetCountOfElementsInDbAndShowFirstAndLastPosterInfo()
                .Select(db => new MoviePosterCountElementInDbAndFirstAndLastElementViewModel
                {
                    Count = db.Count,
                    Name = db.Name,
                    ImageSrc = db.ImageSrc
                })
                .ToList();

            return View(viewModels);
        }
    }
}
