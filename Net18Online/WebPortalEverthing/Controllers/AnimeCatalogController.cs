using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.AnimeCatalog;
using WebPortalEverthing.Services;
using TagGame.Classes.Base;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        private IAnimeCatalogRepositoryReal _animeCatalogRepository;
        private WebDbContext _webDbContext;
        private IWebHostEnvironment _webHostEnvironment;
        private AuthService _authService;
        private TagGameHelper _tagGameHelper;
        private Field _tagField;

        public AnimeCatalogController(IAnimeCatalogRepositoryReal animeCatalogRepository, WebDbContext webDbContext, IWebHostEnvironment webHostEnvironment, AuthService authService, TagGameHelper tagGameHelper, Field field)
        {
            _animeCatalogRepository = animeCatalogRepository;
            _webDbContext = webDbContext;
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
            _tagGameHelper = tagGameHelper;
            _tagField = field;
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

        [IsAuthenticated]
        [HttpGet]
        public IActionResult TagGame()
        {
            var viewModel = new TagGameViewModel();

            var tagsFromDb = _tagGameHelper.GetTagsFromDb(_webDbContext, _authService);

            if (tagsFromDb != null)
            {
                viewModel.Tags = _tagGameHelper.ConvertFlatArrayToMultidimensional(tagsFromDb);

                return View(viewModel);
            }

            _tagField.Change();

            var tags = _tagField.GetTags();

            _tagGameHelper.CahangeTags(_webDbContext, _authService, tags);

            viewModel.Tags = tags;

            return View(viewModel);
        }

        [IsAuthenticated]
        [HttpPost]
        public IActionResult MoveTile([FromBody] MoveTileRequestViewModel request)
        {

            var x = request.x;
            var y = request.y;

            var tagsFromDb = _tagGameHelper.GetTagsFromDb(_webDbContext, _authService);

            var multidimensionalTags = _tagGameHelper.ConvertFlatArrayToMultidimensional(tagsFromDb);

            _tagField.CopyTags(multidimensionalTags);

            var tags = multidimensionalTags;
            var emptyX = -1;
            var emptyY = -1;

            for (var i = 0; i < tags.GetLength(0); i++)
            {
                for (var j = 0; j < tags.GetLength(1); j++)
                {
                    if (tags[i, j] == 0)
                    {
                        emptyX = i;
                        emptyY = j;
                        break;
                    }
                }
            }

            if (emptyX == -1 && emptyY == -1)
            {
                return Json(new { success = false, message = $"Произошла ошибка: Все печально" });
            }

            if (Math.Abs(emptyX - x) + Math.Abs(emptyY - y) != 1)
            {
                return Json(new { success = false, message = "Неверный ход!" });
            }

            _tagField.ChangePositions(x, y);

            var changedTags = _tagField.GetTags();
            _tagGameHelper.CahangeTags(_webDbContext, _authService, changedTags);

            var isWin = _tagField.IsWin();

            var serializableTags = _tagGameHelper.ConvertToSerializableArray(tags);

            return Json(new { success = true, tags = serializableTags, isWin });

        }

        [IsAuthenticated]
        [HttpGet]
        public IActionResult ChangeTags()
        {
            var tagsFromDb = _tagGameHelper.GetTagsFromDb(_webDbContext, _authService);

            var multidimensionalTags = _tagGameHelper.ConvertFlatArrayToMultidimensional(tagsFromDb);

            _tagField.CopyTags(multidimensionalTags);
            _tagField.Change();

            var tags = _tagField.GetTags();
            _tagGameHelper.CahangeTags(_webDbContext, _authService, tags);

            var serializableTags = _tagGameHelper.ConvertToSerializableArray(tags);

            return Json(new { success = true, tags = serializableTags });
        }

    }
}
