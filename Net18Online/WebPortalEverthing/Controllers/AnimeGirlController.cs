using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Controllers
{
    public class AnimeGirlController : Controller
    {
        private IAnimeGirlRepository _animeGirlRepository;

        public AnimeGirlController(IAnimeGirlRepository animeGirlRepository)
        {
            _animeGirlRepository = animeGirlRepository;
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

        public IActionResult AllGirls(int? count)
        {
            if (!_animeGirlRepository.Any())
            {
                GenerateDefaultAnimeGirl(count);
            }

            var girlsFromDb = _animeGirlRepository.GetAll();

            var girlsViewModels = girlsFromDb
                .Select(dbGirl =>
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        ImageSrc = dbGirl.ImageSrc,
                        Tags = dbGirl.Tags
                    }
                )
                .ToList();

            return View(girlsViewModels);
        }

        private void GenerateDefaultAnimeGirl(int? count)
        {
            for (int i = 0; i < (count ?? 4); i++)
            {
                var girlNumber = (i % 4) + 1;
                var dataModel = new GirlData
                {
                    Name = $"Girl {girlNumber}",
                    ImageSrc = $"/images/AnimeGirl/Girl{girlNumber}.jpg",
                    Tags = new List<string> { "4 size", "red" }
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
                Tags = new()
            };

            _animeGirlRepository.Add(dataGirl);

            return RedirectToAction("AllGirls");
        }
    }
}
