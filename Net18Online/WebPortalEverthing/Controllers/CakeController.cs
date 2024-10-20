using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.Cake;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private static List<CakeViewModel> _listCakes = new();
        public IActionResult Index()
        {
            if (!_listCakes.Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    var cake = new CakeViewModel()
                    {
                        ImageSrc = $"/images/Cake/Cake{i + 1}.jpg",
                        Description = "Cake yami",
                        Reiting = 5,
                        Price = 12.45f,
                    };
                    _listCakes.Add(cake);
                }
            }
            return View(_listCakes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CakeCreationViewModel viewModel)
        {
            var cake = new CakeViewModel
            {
                ImageSrc = viewModel.Url,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Reiting = 0,
            };

            _listCakes.Add(cake);

            return RedirectToAction("Index");
        }
    }
}
