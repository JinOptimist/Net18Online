using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.Cake;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private ICakeRepository _cakeRepository;

        public CakeController(ICakeRepository cakeRepository) 
        { 
            _cakeRepository = cakeRepository;
        }
        public IActionResult Index()
        {
            if (!_cakeRepository.Any())
            {
                GenerateDefaultCake();
            }

            var cakesFromDb = _cakeRepository.GetAll();

            var cakesViewModel = cakesFromDb
                .Select(dbCake => new CakeViewModel
                {
                    Id = dbCake.Id,
                    ImageSrc = dbCake.ImageSrc,
                    Description = dbCake.Description,
                    Rating = dbCake.Rating,
                    Price = dbCake.Price
                }).ToList();

            return View(cakesViewModel);
        }

        private void GenerateDefaultCake()
        {
            for (int i = 0; i < 4; i++)
            {
                var cake = new CakeData()
                {
                    ImageSrc = $"/images/Cake/Cake{i + 1}.jpg",
                    Description = "Cake yami",
                    Rating = 5,
                    Price = 12.45m,
                };
                _cakeRepository.Add(cake);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CakeCreationViewModel viewModel)
        {
            var cake = new CakeData
            {
                ImageSrc = viewModel.Url,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Rating = 0,
            };

            _cakeRepository.Add(cake);

            return RedirectToAction("Index");
        }
    }
}
