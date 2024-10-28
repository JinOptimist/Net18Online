using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.SimulatorOfPrinting.TextProvider;
using WebPortalEverthing.Models.SimulatorOfPrinting;

namespace WebPortalEverthing.Controllers
{
    public class SimulatorOfPrintingController : Controller
    {
        private TextProvider _textProvider;

        public SimulatorOfPrintingController(TextProvider textProvider)
        {
            _textProvider = textProvider;
        }
        private static Stopwatch stopwatch = new Stopwatch();

        [HttpGet]
        public IActionResult TextToTypping()
        {
            var model = new TypingViewModel
            {
                CurrentText = textProvider.GetText(),
                UserInput = string.Empty,
                Result = string.Empty
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult TextToTypping(TypingViewModel model)
        {
            stopwatch.Stop();
            model.ElapsedTime = stopwatch.Elapsed;

            if (model.UserInput == model.CurrentText)
            {
                model.Result = "Correct!";
            }
            else
            {
                model.Result = "Incorrect!";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult StartTimer()
        {
            stopwatch.Start();
            return Ok();
        }

        [HttpPost]
        public IActionResult StopTimer()
        {
            stopwatch.Stop();
            return Ok();
        }
    }
}