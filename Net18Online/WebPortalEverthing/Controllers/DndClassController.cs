using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.DND;

namespace WebPortalEverthing.Controllers
{
    public class DndClassController : Controller
    {
        private int DEFAULT_DndClass_COUNT = 2;
        private IDndClassRepositoryReal _dndClassRepository;
        private WebDbContext _webDbContext;
        public DndClassController(IDndClassRepositoryReal dndClassRepository, WebDbContext webDbContext)
        {
            _dndClassRepository = dndClassRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllDndClasses()
        {
            var dndClassesFromDb = _dndClassRepository.GetMostPopular();

            var dndClassViewModels = dndClassesFromDb
                .Select(dbDndClass =>
                    new ClassViewModel
                    {
                        Id = dbDndClass.Id,
                        Name = dbDndClass.Name,
                        ImageSrc = dbDndClass.ImageSrc,
                    }
                )
                .ToList();

            return View(dndClassViewModels);
        }

        private void GenerateDefaultDndClass()
        {
            for (int i = 0; i < DEFAULT_DndClass_COUNT; i++)
            {
                var dndClassNumber = (i % 4) + 1;
                var dataModel = new DndClassData
                {
                    Name = $"DNDClass {dndClassNumber}",
                    ImageSrc = $"/images/DND/DNDClass{dndClassNumber}.jpg",
                };

                _dndClassRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassCreationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dataGirl = new DndClassData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
            };

            _dndClassRepository.Add(dataGirl);

            return RedirectToAction("AllClasses");
        }
        public IActionResult Remove(int id)
        {
            _dndClassRepository.Delete(id);
            return RedirectToAction("AllClasses");
        }
    }
}