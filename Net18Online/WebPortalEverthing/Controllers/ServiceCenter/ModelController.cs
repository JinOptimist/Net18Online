using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.ServiceCenter;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverything.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepositoryReal _modelRepository;
        private readonly IProducerRepositoryReal _producerRepository;
        private readonly ITypeOfApplianceRepositoryReal _typeRepository;

        public ModelController(
            IModelRepositoryReal modelRepository,
            IProducerRepositoryReal producerRepository,
            ITypeOfApplianceRepositoryReal typeRepository)
        {
            _modelRepository = modelRepository;
            _producerRepository = producerRepository;
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public IActionResult AllModels()
        {
            var models = _modelRepository.GetAllModels()
                .Select(model => new ModelShortInfoViewModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    TypeId = model.TypeId,
                    ProducerId = model.ProducerId,
                    ProducerName = model.ModelProducer?.Producer ?? "Unknown",  
                    TypeName = model.ModelType?.Name ?? "Unknown"               
                })
                .ToList();  

            return View(models);
        }

        [HttpGet]
        public IActionResult CreateModel()
        {
            var viewModel = new CreateModelViewModel
            {
                Producers = _producerRepository
                .GetAll()
                .Select(p => new ProducerViewModel
                {
                    Id = p.Id,
                    ProducerName = p.Producer
                }).ToList(),
                Types = _typeRepository
                .GetAll()
                .Select(t => new TypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateModel(CreateModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newModel = new ModelData
                {
                    Name = model.Name,
                    ProducerId = model.ProducerId,
                    TypeId = model.TypeId
                };

                _modelRepository.Add(newModel);
                return RedirectToAction("AllModels");
            }

            model.Producers = _producerRepository
                .GetAll()
                .Select(p => new ProducerViewModel
            {
                Id = p.Id,
                ProducerName = p.Producer
            }).ToList();

            model.Types = _typeRepository
                .GetAll()
                .Select(t => new TypeViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateName(int id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                ModelState.AddModelError("", "Name cannot be empty.");
                return RedirectToAction("AllModels");
            }

            _modelRepository.UpdateName(id, newName);
            return RedirectToAction("AllModels");
        }
    }
}
