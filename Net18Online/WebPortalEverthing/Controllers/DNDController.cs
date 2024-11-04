using Everything.Data;
using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.DND;

namespace WebPortalEverthing.Controllers
{
    public class DNDController : Controller
    {
        private int DEFAULT_GIRL_COUNT = 4;
        private IDndClassRepositoryReal _dndClassRepository;
        private WebDbContext _webDbContext;
        public DNDController(IDndClassRepositoryReal dndClassRepository, WebDbContext webDbContext)
        {
            _dndClassRepository = dndClassRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClasses()
        {
            var dndClassesFromDb = _dndClassRepository.GetMostPopular();

            var dndClassesViewModels = dndClassesFromDb
                .Select(dbDndClass =>
                    new ClassViewModel
                    {
                        Id = dbDndClass.Id,
                        Name = dbDndClass.Name,
                        ImageSrc = dbDndClass.ImageSrc,
                        Tags = new List<string>() //dbDndClass.Tags
                    }
                )
                .ToList();

            return View(dndClassesViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClassCreationViewModel viewModel)
        {
            var dataDndClass = new DndClassData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
            };

            _dndClassRepository.Add(dataDndClass);

            return RedirectToAction("AllClasses");
        }

        public IActionResult UpdateName(string newName, int id)
        {
            _dndClassRepository.UpdateName(id, newName);
            return RedirectToAction("AllClasses");
        }

        public IActionResult UpdateImage(int id, string url)
        {
            _dndClassRepository.UpdateImage(id, url);
            return RedirectToAction("AllClasses");
        }

        public IActionResult Remove(int id)
        {
            _dndClassRepository.Delete(id);
            return RedirectToAction("AllClasses");
        }

    }
}