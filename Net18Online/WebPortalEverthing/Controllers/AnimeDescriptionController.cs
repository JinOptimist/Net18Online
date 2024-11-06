using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeCatalog;
using WebPortalEverthing.Models.AnimeDescription;

namespace WebPortalEverthing.Controllers
{
    public class AnimeDescriptionController : Controller
    {
        private IAnimeDescriptionRepositoryReal _animeDescriptionRepositoryReal;
        private IAnimeCatalogRepositoryReal _animeCatalogRepositoryReal;

        public AnimeDescriptionController(IAnimeDescriptionRepositoryReal animeDescriptionRepositoryReal, IAnimeCatalogRepositoryReal animeCatalogRepositoryReal)
        {
            _animeDescriptionRepositoryReal = animeDescriptionRepositoryReal;
            _animeCatalogRepositoryReal = animeCatalogRepositoryReal;
        }

        public IActionResult Index()
        {
            var animeDescriptionViewModels = _animeDescriptionRepositoryReal
                .GetAllWithAnimes()
                .Select(x => new AnimeDescriptionShortInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Animes = x.Animes.Select(animeData =>
                        new AnimeCatalogNameAndIdViewModel
                        {
                            Id = animeData.Id,
                            Name = animeData.Name,
                        }).ToList()
                })
                .ToList();

            var animeViewModels = _animeCatalogRepositoryReal
                .GetWithoutDescription()
                .Select(x => new AnimeCatalogNameAndIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            var viewModel = new IndexAnimeDescriptionViewModel
            {
                Descriptions = animeDescriptionViewModels,
                Animes = animeViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnimeDescriptionViewModel viewModel)
        {
            var animeDescription = new AnimeDescriptionData
            {
                Name = viewModel.Name,
                Description = viewModel.Description
            };

            _animeDescriptionRepositoryReal.Add(animeDescription);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LinkAnimeAndDescription(int animeDescriptionId, int animeId)
        {
            _animeDescriptionRepositoryReal.LinkAnime(animeDescriptionId, animeId);
            return RedirectToAction("Index");
        }
    }
}
