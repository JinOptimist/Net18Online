using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPortalEverthing.Models.Ecology;

namespace WebPortalEverthing.Controllers
{
    public class EcologyController : Controller
    {
        // BAD. DO NOT USE THIS ON PROD
        private static List<EcologyViewModel> ecologylViewModels = new List<EcologyViewModel>();

        /*private readonly ILogger<EcologyController> _logger;

        public EcologyController(ILogger<EcologyController> logger)
        {
            _logger = logger;
        }*/

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
            var ecology = new EcologyViewModel
            {
                ImageSrc = viewModel.Url,
                Texts = new List<string>(),
            };
            ecologylViewModels.Add(ecology);

            return View(ecologylViewModels);
        }
    }
}