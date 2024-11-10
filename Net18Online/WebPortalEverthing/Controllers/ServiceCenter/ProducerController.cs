using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverything.Controllers.ServiceCenter
{
    public class ProducerController : Controller
    {
        private readonly IProducerRepositoryReal _producerRepository;
        private WebDbContext _webDbContext;

        public ProducerController(IProducerRepositoryReal producerRepository, WebDbContext webDbContext)
        {
            _producerRepository = producerRepository;
            _webDbContext = webDbContext;
        }

        public IActionResult AllProducers()
        {
            var producers = _producerRepository.GetAll().Select(p => new ProducerShortInfoViewModel
            {
                Id = p.Id,
                ProducerName = p.Producer,
                Models = p.ModelsOnProducer.Select(m => new ModelShortInfoViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    TypeId = m.TypeId
                }).ToList()
            }).ToList();

            var viewModel = new IndexProducerViewModel
            {
                Producers = producers,
                Models = producers.
                    SelectMany(p => p.Models).
                    ToList() 
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateProducer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProducer(CreateProducerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var producerData = new ProducerData
                {
                    Producer = model.ProducerName
                };

                _producerRepository.Add(producerData);
                return RedirectToAction("AllProducers");
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProducerName(int id, string newProducerName)
        {
            if (string.IsNullOrWhiteSpace(newProducerName))
            {
                return BadRequest("Producer name cannot be empty.");
            }

            _producerRepository.UpdateProducerName(id, newProducerName);
            return RedirectToAction("AllProducers");
        }
    }
}
