﻿using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.GameStore;
using WebPortalEverthing.Models;
using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
//using Everything.Data.Fake.Models;
using Everything.Data;
using Everything.Data.Repositories;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPortalEverthing.Controllers.AuthAttributes;

namespace WebPortalEverthing.Controllers
{
    public class GameStoreController : Controller
    {
        private IGameStoreRepositoryReal _gameStoreRepository;
        private WebDbContext _webDbContext;
        private CheckingForBannedNames _checkingForBannedNames = new();
        private List<string> _bannedWords = new List<string> { "admin", "root", "test" };
        private AuthService _authService;

        public GameStoreController(IGameStoreRepositoryReal gameStoreRepository, WebDbContext webDbContext, AuthService authService)
        {
            _gameStoreRepository = gameStoreRepository;
            _webDbContext = webDbContext;
            _authService = authService;
        }
        public IActionResult Index()
        {
            var model = new GameViewModel();
            var userName = _authService.GetName();
            model.UserName = userName;
            model.Date = DateTime.Now;
            model.NameGame = "Dota 2";

            return View(model);
        }

        public IActionResult Shop()
        {
            var gameFromDb = _gameStoreRepository.GetAll();

            if (!_gameStoreRepository.Any())
            {                
                //GenerateDefaultGame();
            }

            var shopViewModels = gameFromDb.Select(dbGame =>
            new ShopViewModel
            {
                Id = dbGame.Id,
                NameGame = dbGame.NameGame,
                ImageSrc = dbGame.ImageSrc,
                Cost = dbGame.Cost,                
            }).ToList();

            var shopListViewModel = new ShopListViewModel
            {
                Games = shopViewModels,
            };

            return View(shopListViewModel);
        }
        [HttpGet]
        public IActionResult Library()
        {
            var buyerId = _authService.GetUserId();
            var gameFromDb = _gameStoreRepository.GetGameWithBuyer((int)buyerId!);            
            if (!_gameStoreRepository.Any())
            {
                //GenerateDefaultGame();
            }

            var gameViewModels = gameFromDb.Select(dbGame =>
            new GameViewModel
            {              
                NameGame = dbGame.Name,
                ImageSrc = dbGame.ImageSrc,
                Tags = new(),
            }
            ).ToList();

            var libraryViewModel = new LibraryViewModel
            {
                GamesInTheLibrary = gameViewModels,
            };
            return View(libraryViewModel);


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
                    //Tags = new List<string> { "Added", "Installed" }
                };
                _gameStoreRepository.Add(dataModel);
            }
        }

        [HttpGet]
        [IsAdmin]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [IsAdmin]
        public IActionResult Add(AddGameViewModel viewModel)
        {
            if (_gameStoreRepository.HasSimilarName(viewModel.Name))
            {
                ModelState.AddModelError(
                    nameof(AddGameViewModel.Name),
                    "Слишком похожее имя");
            }

            if (_checkingForBannedNames.HasBannedName(viewModel.Name, _bannedWords))
            {
                ModelState.AddModelError(
                    nameof(AddGameViewModel.Name),
                    "Запрещенное имя");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var dataGame = new GameData
            {
                NameGame = viewModel.Name,
                ImageSrc = viewModel.Url,
                Cost = viewModel.Cost,
                //Tags = new()
            };


            _gameStoreRepository.Add(dataGame);

            return RedirectToAction("Shop");
        }
        public IActionResult UpdateName(string newName, int id)
        {
            _gameStoreRepository.UpdateName(id, newName);
            return RedirectToAction("Shop");
        }

        public IActionResult UpdateImage(int id, string url)
        {
            _gameStoreRepository.UpdateImage(id, url);
            return RedirectToAction("Shop");
        }

        
        public IActionResult Purchases(int buyerId, int gameId)
        {

            _gameStoreRepository.LinkGame(buyerId, gameId);
            return RedirectToAction("Shop");

        }
        public IActionResult Remove(int id)
        {
            _gameStoreRepository.Delete(id);
            return RedirectToAction("Library");
        }
    }
}
