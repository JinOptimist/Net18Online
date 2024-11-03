using Everything.Data;
using Everything.Data.Interface.Models;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Controllers
{
    public class AnimeGirlController : Controller
    {
        private int DEFAULT_GIRL_COUNT = 4;
        private IAnimeGirlRepositoryReal _animeGirlRepository;
        private WebDbContext _webDbContext;
        public AnimeGirlController(IAnimeGirlRepositoryReal animeGirlRepository, WebDbContext webDbContext)
        {
            _animeGirlRepository = animeGirlRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index(string name, int age)
        {
            var model = new AnimeGirlIndexViewModel();
            model.Name = name ?? "Ivan";
            model.Age = age;
            model.Hours = DateTime.Now.Hour;
            model.Minutes = DateTime.Now.Minute;
            model.Seconds = DateTime.Now.Second;
            return View(model);
        }

        public IActionResult AllGirls()
        {
            if (!_animeGirlRepository.Any())
            {
                GenerateDefaultAnimeGirl();
            }

            var girlsFromDb = _animeGirlRepository.GetMostPopular();

            var girlsViewModels = girlsFromDb
                .Select(dbGirl =>
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        ImageSrc = dbGirl.ImageSrc,
                        Tags = new List<string>() //dbGirl.Tags
                    }
                )
                .ToList();

            return View(girlsViewModels);
        }

        private void GenerateDefaultAnimeGirl()
        {
            for (int i = 0; i < DEFAULT_GIRL_COUNT; i++)
            {
                var girlNumber = (i % 4) + 1;
                var dataModel = new GirlData
                {
                    Name = $"Girl {girlNumber}",
                    ImageSrc = $"/images/AnimeGirl/Girl{girlNumber}.jpg",
                    // Tags = new List<string> { "4 size", "red" }
                };

                _animeGirlRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GirlCreationViewModel viewModel)
        {
            var dataGirl = new GirlData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
            };

            _animeGirlRepository.Add(dataGirl);

            return RedirectToAction("AllGirls");
        }
        
        public IActionResult UpdateName(string newName, int id)
        {
            _animeGirlRepository.UpdateName(id, newName);
            return RedirectToAction("AllGirls");
        }

        public IActionResult UpdateImage(int id, string url)
        {
            _animeGirlRepository.UpdateImage(id, url);
            return RedirectToAction("AllGirls");
        }

        public IActionResult Remove(int id)
        {
            _animeGirlRepository.Delete(id);
            return RedirectToAction("AllGirls");
        }
    }
}
