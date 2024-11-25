using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.DND;

namespace WebPortalEverthing.Controllers
{
    public class DNDController : Controller
    {
        private int DEFAULT_Class_COUNT = 4;
        private IDNDRepository _dndRepository;
        private WebDbContext _webDbContext;
        public DNDController(IDNDRepository dndRepository, WebDbContext webDbContext)
        {
            _dndRepository = dndRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllClasses()
        {
            //if (!_dndRepository.Any())
            //{
            //    GenerateDefaultDndClass();
            //}
            //
            //var classesFromDb = _dndRepository.GetAll();
            var dndClassesFromRealDb = _webDbContext
                .DndClasses
                .Where(x => x.Id > 3)
                .ToList();

            var classViewModel = dndClassesFromRealDb
                .Select(dbClass =>
                    new ClassViewModel
                    {
                        Id = dbClass.Id,
                        Name = dbClass.Name,
                        ImageSrc = dbClass.ImageSrc,
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
                var dataModel = new DndClassData
                {
                    Name = $"DNDClass {classNumber}",
                    ImageSrc = $"/images/DND/DNDClass{classNumber}.jpg",
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
            var dataDndClass = new DndClassData
            {
                Name = viewModel.Name,
                ImageSrc = viewModel.Url,
            };


            _webDbContext.DndClasses.Add(dataDndClass);
            _webDbContext.SaveChanges();

            //_dndRepository.Add(dataDndClass);

            return RedirectToAction("AllClasses");
        }
    }
}