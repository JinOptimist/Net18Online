using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.Manga;

namespace WebPortalEverthing.Controllers
{
    public class MangaController : Controller
    {
        private IMangaRepositoryReal _mangaRepositoryReal;
        private IAnimeGirlRepositoryReal _animeGirlRepositoryReal;

        public MangaController(IMangaRepositoryReal mangaRepositoryReal, IAnimeGirlRepositoryReal animeGirlRepositoryReal)
        {
            _mangaRepositoryReal = mangaRepositoryReal;
            _animeGirlRepositoryReal = animeGirlRepositoryReal;
        }

        public IActionResult Index()
        {
            var mangaViewModels = _mangaRepositoryReal
                .GetAllWithCharacters()
                .Select(x => new MangaShortInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Girls = x.Characters.Select(girlData =>
                        new GirlNameAndIdViewModel
                        {
                            Id = girlData.Id,
                            Name = girlData.Name,
                        }).ToList()
                })
                .ToList();

            var girlViewModels = _animeGirlRepositoryReal
                .GetWithoutManga()
                .Select(x => new GirlNameAndIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            var viewModel = new IndexMangaViewModel
            {
                Mangas = mangaViewModels,
                Girls = girlViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMangaViewModel viewModel)
        {
            var manga = new MangaData
            {
                Title = viewModel.Title,
                Description = viewModel.Description
            };

            _mangaRepositoryReal.Add(manga);

            return RedirectToAction("IndexLoadVolume");
        }

        [HttpPost]
        public IActionResult LinkGirlAndManga(int mangaId, int girlId)
        {
            _mangaRepositoryReal.LinkGirl(mangaId, girlId);
            return RedirectToAction("IndexLoadVolume");
        }
    }
}
