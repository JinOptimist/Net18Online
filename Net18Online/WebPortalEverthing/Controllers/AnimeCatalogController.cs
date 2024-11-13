using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        private IAnimeCatalogRepositoryReal _animeCatalogRepository;

        private WebDbContext _webDbContext;

        public AnimeCatalogController(IAnimeCatalogRepositoryReal animeCatalogRepository, WebDbContext webDbContext)
        {
            _animeCatalogRepository = animeCatalogRepository;
            _webDbContext = webDbContext;
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

        [HttpPost]
        public IActionResult Create(AnimeCatalogCreationViewModel viewModel)
        {
            var dataCatalog = new AnimeData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.ImageSrc,
            };

            _animeCatalogRepository.Add(dataCatalog);

            return RedirectToAction("Index");
        }
    }
}
