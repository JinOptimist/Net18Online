using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.Cake;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private ICakeRepositoryReal _cakeRepository;
        private WebDbContext _webDbContext;

        public CakeController(ICakeRepositoryReal cakeRepository, WebDbContext webDbContext)
        {
            _cakeRepository = cakeRepository;
            _webDbContext = webDbContext;
        }
        public IActionResult Index()
        {
            var cakesFromDb = _webDbContext
                .Cakes;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CakeCreationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

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
        public IActionResult UpdateDescription(int id, string newDescription)
        {
            _cakeRepository.UpdateDescription(id, newDescription);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cakeRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
