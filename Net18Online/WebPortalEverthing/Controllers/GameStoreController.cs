﻿using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.GameStore;
using WebPortalEverthing.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Fake.Models;

namespace WebPortalEverthing.Controllers
{
    public class GameStoreController : Controller
    {
        private IGameStoreRepository _gameStoreRepository;

        public GameStoreController(IGameStoreRepository gameStoreRepository)
        {
            _gameStoreRepository = gameStoreRepository;
        }
        public IActionResult Index()
        {
            var model = new GameViewModel();

            model.Date = DateTime.Now;
            model.NameGame = "Dota 2";

            return View(model);
        }

        public IActionResult Shop()
        {            
            return View();
        }
        public IActionResult Library()
        {
            var gameFromDb = _gameStoreRepository.GetAll();

            if (!_gameStoreRepository.Any())
            {
                GenerateDefaultGame();
            }

            var gameViewModels = gameFromDb.Select(dbGame =>
            new GameViewModel
            {
                NameGame = dbGame.NameGame,
                ImageSrc = dbGame.ImageSrc,
                Tags = dbGame.Tags,
            }
            ).ToList();
            return View(gameViewModels);
           
            
        }

        private void GenerateDefaultGame()
        {
            for (int i = 0; i < 6; i++)
            {
                var gameNumber = (i % 6) + 1;
                var dataModel = new GameData()
                {
                    NameGame = $"Game {gameNumber}",
                    ImageSrc = $"/images/GameStore/Game{gameNumber}.jpg",
                    Tags = new List<string> { "Added", "Installed" }
                };
                _gameStoreRepository.Add(dataModel);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddGameViewModel viewModel)
        {
            var dataGame = new GameData
            {
                NameGame = viewModel.Name,
                ImageSrc = viewModel.Url,
                Tags = new()
            };
            
            _gameStoreRepository.Add(dataGame);

            return RedirectToAction("Library");
        }
    }
}