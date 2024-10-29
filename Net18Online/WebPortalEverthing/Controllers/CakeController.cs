using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Cake;
using Everything.Data;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private ICakeRepository _cakeRepository;
        private WebDbContext _webDbContext;

        public CakeController(ICakeRepository cakeRepository, WebDbContext webDbContext)
        {
            _cakeRepository = cakeRepository;
            _webDbContext = webDbContext;
        }
        public IActionResult Index()
        {
            var cakesFromDb = _webDbContext
                .Cakes
                .ToList();

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

            _webDbContext.Cakes.Add(cake);
            _webDbContext.SaveChanges();

            //_cakeRepository.Add(cake);

            return RedirectToAction("Index");
        }
    }
}
