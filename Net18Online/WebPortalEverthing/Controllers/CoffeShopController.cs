using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.CoffeShop;
using Everything.Data;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {
        private ICoffeShopRepository _coffeShopRepository;
        private WebDbContext _webDbContext;

        public CoffeShopController(ICoffeShopRepository coffeShopRepository, WebDbContext webDbContext)
        {
            _coffeShopRepository = coffeShopRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        public List<CoffeViewModel> CoffeView()
        {
            var valuesCoffeFromDb = _webDbContext
                .Coffe
                .ToList();

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
            //if (!_coffeShopRepository.Any())
            //{
            //    DefaultCoffeViewValue();
            //}

            var viewModels = CoffeView();

            return View(viewModels);
        }

        //public void DefaultCoffeViewValue()
        //{
        //    var costOfAmericano = 1;
        //    var AmericanoViewModel = new CoffeData
        //    {
        //        Coffe = "Americano",
        //        Url = $"/images/CoffeShop/Americano.jpeg",
        //        Cost = costOfAmericano,
        //    };

        //    _coffeShopRepository
        //        .Add(AmericanoViewModel);
        //}

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

            _webDbContext
                .Add(coffe);
            _webDbContext
                .SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var coffeItem = _webDbContext.Coffe.Find(id);

            if (coffeItem != null)
            {
                _webDbContext.Coffe
                    .Remove(coffeItem);
                _webDbContext
                    .SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var coffeItem = _webDbContext.Coffe.Find(id);

            var newCoffeItem = new CoffeData
            {
                Brand = coffeItem.Brand,
                Coffe = coffeItem.Coffe,
                Url = coffeItem.Url,
                Cost = coffeItem.Cost
            };

            return View(newCoffeItem);
        }

        [HttpPost]
        public IActionResult Update(int id, CoffeUpdateViewModel viewModel)
        {
            var coffeItem = _webDbContext.Coffe.Find(id);

            coffeItem.Coffe = viewModel.Coffe;
            coffeItem.Url = viewModel.Url;  
            coffeItem.Cost = viewModel.Cost;
            coffeItem.Brand = viewModel.Brand;

            _webDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}