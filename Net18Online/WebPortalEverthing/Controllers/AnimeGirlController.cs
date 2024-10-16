using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Controllers
{
    public class AnimeGirlController : Controller
    {
        // BAD. DO NOT USE THIS ON PROD
        private static List<GirlViewModel> girlViewModels = new List<GirlViewModel>();

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
            if (!girlViewModels.Any())
            {
                for (int i = 0; i < (count ?? 4); i++)
                {
                    var girlNumber = (i % 4) + 1;
                    var viewModel = new GirlViewModel
                    {
                        Name = $"Girl {girlNumber}",
                        ImageSrc = $"/images/AnimeGirl/Girl{girlNumber}.jpg",
                        Tags = new List<string> { "4 size", "red" }
                    };
                    girlViewModels.Add(viewModel);
                }
            }


            return View(girlViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GirlCreationViewModel viewModel)
        {
            var girl = new GirlViewModel
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = new List<string>()
            };

            girlViewModels.Add(girl);

            return RedirectToAction("AllGirls");
        }
    }
}
