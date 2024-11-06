using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Chess;

namespace WebProject.Controllers
{
    public class ChessController : Controller
    {

        private static List<PartiesViewModel> partiesViewModels = new List<PartiesViewModel>();
        public IActionResult Index(string name)
        {
            var model = new ChessViewModel();
            model.Name = name;
            return View(model);
        }

        public IActionResult Rating()
        {
            var model = new ChessViewModel();
            model.Name = "Sasha";
            return View(model);
        }

        public IActionResult News()
        {
            return View(partiesViewModels);
        }

        [HttpGet]
        public IActionResult Parties()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Parties(PartiesCreateViewModels model)
        {
            var party = new PartiesViewModel
            {
                Name = model.Name,
                Color = model.Color,
                Winner = model.Winner,
            };

            partiesViewModels.Add(party); 

            return RedirectToAction("News");
        }
    }
}
