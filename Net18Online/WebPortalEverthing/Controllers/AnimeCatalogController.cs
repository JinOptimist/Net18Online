using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        private IAnimeCatalogRepository _animeCatalogRepository;

        public AnimeCatalogController(IAnimeCatalogRepository animeCatalogRepository)
        {
            _animeCatalogRepository = animeCatalogRepository;
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
                var dataModel = new AnimeCatalogData
                {
                    Name = $"Anime {animeNumber}",
                    ImageSrc = $"/images/AnimeCatalog/Anime{animeNumber}.jpg",
                };

                _animeCatalogRepository.Add(dataModel);
            }
        }
    }
}
