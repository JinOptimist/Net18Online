using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPortalEverthing.Models.Ecology;

namespace WebPortalEverthing.Controllers
{
    public class EcologyController : Controller
    {
        private IEcologyRepository _ecologyRepository;

        public EcologyController(IEcologyRepository ecologyRepository)
        {
            _ecologyRepository = ecologyRepository;
        }

        public IActionResult Index()
        {
            var model = new EcologyViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult EcologyChat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EcologyChat(PostCreationViewModel viewModel)
        {
            var ecology = new EcologyData
            {
                ImageSrc = viewModel.Url,
                Text = new List<string>{viewModel.Text},
            };
            _ecologyRepository.Add(ecology);

            var ecologyFromDb = _ecologyRepository.GetAll();

            var ecologyViewModels = ecologyFromDb
                .Select(dbEcology =>
                    new EcologyViewModel
                    {
                        ImageSrc = dbEcology.ImageSrc,
                        Texts = dbEcology.Text
                    }
                )
                .ToList();

            return View(ecologyViewModels);
        }
    }
}