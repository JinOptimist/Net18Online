using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models;
using WebPortalEverthing.Models.DND;

namespace WebPortalEverthing.Controllers
{
    public class DNDController : Controller
    {
        // BAD. DO NOT USE THIS ON PROD
        private static List<ClassViewModel> classViewModel = new List<ClassViewModel>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClasses(int? count)
        {
            if (!classViewModel.Any())
            {
                for (int i = 0; i < (count ?? 4); i++)
                {
                    var classNumber = (i % 4) + 1;
                    var viewModel = new ClassViewModel
                    {
                        Name = $"Class {classNumber}",
                        ImageSrc = $"/images/DND/DNDClass{classNumber}.jpg",
                        Tags = new List<string> { "4 size", "red" }
                    };
                    classViewModel.Add(viewModel);
                }
            }


            return View(classViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassCreationViewModel viewModel)
        {
            var DNDClass = new ClassViewModel
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = new List<string>()
            };

            classViewModel.Add(DNDClass);

            return RedirectToAction("AllGirls");
        }
    }
}