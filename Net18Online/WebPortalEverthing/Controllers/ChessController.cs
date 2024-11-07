
﻿using Everything.Data.Fake.Models;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Chess;

namespace WebProject.Controllers
{
    public class ChessController : Controller
    {


        private IChessPartiesRepository _chessPartiesRepository;

        public ChessController(IChessPartiesRepository chessPartiesRepository)
        {
            _chessPartiesRepository = chessPartiesRepository;
        }

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
            var partiesFromDb = _chessPartiesRepository.GetAll();
            var partiesViewModels = partiesFromDb.Select(DbParties =>
            new PartiesViewModel
            {
                Id = DbParties.Id,
                Name = DbParties.Name,
                Color = DbParties.Color,
                Winner = DbParties.Winner,
            }
            ).ToList();

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

            var party = new PartiesData
            {
                Name = model.Name,
                Color = model.Color,
                Winner = model.Winner,
            };

            _chessPartiesRepository.Add(party); 

            return RedirectToAction("News");
        }
    }
}
