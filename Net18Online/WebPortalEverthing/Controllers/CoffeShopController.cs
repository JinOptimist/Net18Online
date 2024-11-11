﻿using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {
        private IKeyCoffeShopRepository _coffeShopRepository;

        public CoffeShopController(IKeyCoffeShopRepository coffeShopRepository)
        {
            _coffeShopRepository = coffeShopRepository;

        }

        public IActionResult Index()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        public List<CoffeViewModel> CoffeView()
        {
            var valuesCoffeFromDb = _coffeShopRepository.GetAll();

            var viewModels = valuesCoffeFromDb
                .Select(coffeFromDb =>
                    new CoffeViewModel
                    {
                        Id = coffeFromDb.Id,
                        Coffe = coffeFromDb.Coffe,
                        Url = coffeFromDb.Url,
                        Cost = coffeFromDb.Cost,
                    }
                ).ToList();

            return viewModels;
        }

        public IActionResult Coffe()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoffeCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var coffe = new CoffeData
            {
                Coffe = viewModel.Coffe,
                Url = viewModel.Url,
                Cost = viewModel.Cost
            };

            _coffeShopRepository.Add(coffe);

            return RedirectToAction("IndexLoadVolumeView");
        }

        public IActionResult Delete(int id)
        {
            _coffeShopRepository.Delete(id);
            return RedirectToAction("IndexLoadVolumeView");
        }

        public IActionResult UpdateCoffe(int id, string name)
        {
            _coffeShopRepository.UpdateCoffeName(id, name);
            return RedirectToAction("IndexLoadVolumeView");
        }

        public IActionResult UpdateCost(int id, decimal cost)
        {
            _coffeShopRepository.UpdateCost(id, cost);
            return RedirectToAction("IndexLoadVolumeView");
        }

        public IActionResult UpdateUrl(int id, string url)
        {
            _coffeShopRepository.UpdateImage(id, url);
            return RedirectToAction("IndexLoadVolumeView");
        }
    }
}