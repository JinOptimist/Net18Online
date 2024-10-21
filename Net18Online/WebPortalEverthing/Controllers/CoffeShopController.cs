using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {

        /// <summary>
        /// worst idea
        /// </summary>
        private static List<CoffeViewModel> coffeViewModels = new List<CoffeViewModel>();


        public IActionResult Index()
        {
            return View(coffeViewModels);
        }

        public IActionResult Coffe(int? count)
        {
            if (coffeViewModels.Any())
            {
                return View(coffeViewModels);
            }
            for (int i = 0; i < (count ?? 1); i++)
            {
                var AmericanoViewModel = new CoffeViewModel
                {
                    Id = i + 1,
                    Coffe = "Americano",
                    Url = $"/images/CoffeShop/Americano.jpeg",
                    Cost = 1.5f
                };
                coffeViewModels
                    .Add(AmericanoViewModel);

                var RafViewModel = new CoffeViewModel
                {
                    Id = i + 2,
                    Coffe = "Raf",
                    Url = $"/images/CoffeShop/raf.jpg",
                    Cost = 2.0f
                };
                coffeViewModels
                    .Add(RafViewModel);
            }
            return View(coffeViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoffeCreateViewModel viewModel)
        {
            var constId = 3;
            var coffe = new CoffeViewModel
            {
                Id = constId,
                Coffe = viewModel.Coffe,
                Url = viewModel.Url,
                Cost = viewModel.Cost
            };

            coffeViewModels
                .Add(coffe);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var coffeItem = coffeViewModels
                .FirstOrDefault(c => c.Id == id);

            if (coffeItem != null)
            {
                coffeViewModels.Remove(coffeItem);
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
            var coffeItem = coffeViewModels
                .FirstOrDefault(c => c.Id == viewModel.Id);

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