using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {
        private ICoffeShopRepository _coffeShopRepository;

        public CoffeShopController(ICoffeShopRepository coffeShopRepository)
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
            if (!_coffeShopRepository.Any())
            {
                DefaultCoffeViewValue();
            }

            var viewModels = CoffeView();

            return View(viewModels);
        }

        public void DefaultCoffeViewValue()
        {
            var costOfAmericano = 1.0f;
            var AmericanoViewModel = new CoffeData
            {
                Coffe = "Americano",
                Url = $"/images/CoffeShop/Americano.jpeg",
                Cost = costOfAmericano,
            };

            _coffeShopRepository
                .Add(AmericanoViewModel);

            var costOfRaf = 2.0f;
            var RafViewModel = new CoffeData
            {
                Coffe = "Raf",
                Url = $"/images/CoffeShop/raf.jpg",
                Cost = costOfRaf,
            };
            _coffeShopRepository
                .Add(RafViewModel);
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
                Coffe = viewModel.Coffe,
                Url = viewModel.Url,
                Cost = viewModel.Cost
            };

            _coffeShopRepository
                .Add(coffe);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var coffeItem = _coffeShopRepository.Get(id);

            if (coffeItem != null)
            {
                _coffeShopRepository.Delete(coffeItem);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(CoffeUpdateViewModel viewModel)
        {
            var coffeItem = _coffeShopRepository.Get(viewModel.Id);

            if (coffeItem != null)
            {
                coffeItem.Url = viewModel.Url;
                coffeItem.Coffe = viewModel.Coffe;
                coffeItem.Cost = viewModel.Cost;
            }

            return RedirectToAction("Index");
        }
    }
}