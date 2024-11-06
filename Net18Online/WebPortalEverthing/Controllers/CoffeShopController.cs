using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.CoffeShop;
using Everything.Data;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {
        private IKeyCoffeShopRepository _coffeShopRepository;

        public CoffeShopController(IKeyCoffeShopRepository coffeShopRepository)
        {
            _coffeShopRepository = coffeShopRepository;
        }

        public IActionResult Index()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        public List<CoffeViewModel> CoffeView()
        {
            var valuesCoffeFromDb = _coffeShopRepository.GetAll();

            var viewModels = valuesCoffeFromDb
                .Select(coffeFromDb =>
                    new CoffeViewModel
                    {
                        Brand = coffeFromDb.Brand,
                        Id = coffeFromDb.Id,
                        Coffe = coffeFromDb.Coffe,
                        Url = coffeFromDb.Url,
                        Cost = coffeFromDb.Cost,
                    }
                ).ToList();

            return viewModels;
        }

        public IActionResult Coffe()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoffeCreateViewModel viewModel)
        {
            var coffe = new CoffeData
            {
                Brand = viewModel.Brand,
                Coffe = viewModel.Coffe,
                Url = viewModel.Url,
                Cost = viewModel.Cost
            };

            _coffeShopRepository.Add(coffe);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _coffeShopRepository.Delete(id);
            return RedirectToAction("Index");
        }
        
        public IActionResult UpdateCoffe(int id, string name)
        {
            _coffeShopRepository.UpdateCoffeName(id, name);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCost(int id, decimal cost)
        {
            _coffeShopRepository.UpdateCost(id, cost);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateBrand(int id, string brand)
        {
            _coffeShopRepository.UpdateBrand(id, brand);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateUrl(int id, string url)
        {
            _coffeShopRepository.UpdateImage(id, url);
            return RedirectToAction("Index");
        }
    }
}