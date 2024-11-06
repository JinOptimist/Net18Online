using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Brand;
using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Controllers
{
    public class BrandController : Controller
    {
        private IBrandRepositoryReal _brandRepositoryReal;
        private IKeyCoffeShopRepository _keyCoffeShopRepository;


        public BrandController(IBrandRepositoryReal brandRepositoryReal, IKeyCoffeShopRepository keyCoffeShopRepository)
        {
            _brandRepositoryReal = brandRepositoryReal;
            _keyCoffeShopRepository = keyCoffeShopRepository;
        }

        public IActionResult Index()
        {
            var brandsViewModel = _brandRepositoryReal
                .GetAllWithCoffe()
                .Select(x =>
                    new BrandShortInfoViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Company = x.Company,
                        CoffeList = x.Coffe.Select(coffeData =>
                                new CoffeNameAndIdViewModel
                                {
                                    Id = coffeData.Id,
                                    Name = coffeData.Coffe
                                }).ToList()
                    })
                .ToList();

            var coffeViewModel = _keyCoffeShopRepository
                .GetAll()
                .Select(x =>
                    new CoffeNameAndIdViewModel
                    {
                        Id = x.Id,
                        Name = x.Coffe,
                    })
                .ToList();

            var viewModel = new BrandViewModel
            {
                Brands = brandsViewModel,
                Coffe = coffeViewModel
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBrandViewModel viewModel)
        {
            var brand = new BrandData
            {
                Name = viewModel.Name,
                Company = viewModel.Company,
            };


            _brandRepositoryReal.Add(brand);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LinkCoffeAndBrand(int brandId, int coffeId)
        {
            _brandRepositoryReal.LinkCoffe(brandId, coffeId);
            return RedirectToAction("Index");
        }
    }
}
