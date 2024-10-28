using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.DND;

namespace WebPortalEverthing.Controllers
{
    public class DNDController : Controller
    {
        private int DEFAULT_Class_COUNT = 4;
        private IDNDRepository _dndRepository;
        public DNDController(IDNDRepository dndRepository)
        {
            _dndRepository = dndRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClasses()
        {
            if (!_dndRepository.Any())
            {
                GenerateDefaultDndClass();
            }

            var classesFromDb = _dndRepository.GetAll();

            var classViewModel = classesFromDb
                .Select(dbClass =>
                    new ClassViewModel
                    {
                        Name = dbClass.Name,
                        ImageSrc = dbClass.ImageSrc,
                        Tags = dbClass.Tags
                    }
                )
                .ToList();

            return View(classViewModel);
        }

        private void GenerateDefaultDndClass()
        {
            for (int i = 0; i < DEFAULT_Class_COUNT; i++)
            {
                var classNumber = (i % 4) + 1;
                var dataModel = new DNDData
                {
                    Name = $"DNDClass {classNumber}",
                    ImageSrc = $"/images/DND/DNDClass{classNumber}.jpg",
                    Tags = new List<string> { "4 size", "red" }
                };

                _dndRepository.Add(dataModel);
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
            var dataDndClass = new DNDData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = new List<string>() { "" }
            };

            _dndRepository.Add(dataDndClass);

            return RedirectToAction("ClassCreationViewModel");
        }
    }
}