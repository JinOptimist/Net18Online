using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.GameStore;
using WebPortalEverthing.Models;
using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using System;
//using Everything.Data.Fake.Models;
using Everything.Data;
using Everything.Data.Repositories;
using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.AnimeGirl.Profile;
using Enums.Users;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Hubs;

namespace WebPortalEverthing.Controllers
{
    public class GameStoreController : Controller
    {
        private IGameStoreRepositoryReal _gameStoreRepository;
        private IUserRepositryReal _userRepositryReal;
        private WebDbContext _webDbContext;
        private CheckingForBannedNames _checkingForBannedNames = new();
        private List<string> _bannedWords = new List<string> { "admin", "root", "test" };
        private AuthService _authService;
        private IWebHostEnvironment _webHostEnvironment;
        private IHubContext<GameAlertHub, IGameAlertHub> _hubContext;

        public GameStoreController(IGameStoreRepositoryReal gameStoreRepository,
            WebDbContext webDbContext,
            AuthService authService,
            IUserRepositryReal userRepositryReal,
            IWebHostEnvironment webHostEnvironment,
            IHubContext<GameAlertHub, IGameAlertHub> hubContext)
        {
            _gameStoreRepository = gameStoreRepository;
            _webDbContext = webDbContext;
            _authService = authService;
            _userRepositryReal = userRepositryReal;
            _webHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
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
            var gameFromDb = _gameStoreRepository.GetAllWithStudio();

            if (!_gameStoreRepository.Any())
            {
                //GenerateDefaultGame();
            }

            var userId = _authService.GetUserId()!;
            var user = _userRepositryReal.Get(userId.Value)!;

            var shopViewModels = gameFromDb.Select(dbGame =>
            new ShopViewModel
            {
                Id = dbGame.Id,
                NameGame = dbGame.NameGame,
                ImageSrc = dbGame.ImageSrc,
                Cost = dbGame.Cost,
                Studios = dbGame.Studios?.Name, 
                LikeCount = dbGame.UsersWhoLikedGame.Count(),
                IsLiked = dbGame.UsersWhoLikedGame.Any(x => x.Id == user.Id),
                DislikeCount =  dbGame.UsersWhoDislikedGame.Count(),
                IsDisliked = dbGame.UsersWhoDislikedGame.Any(x => x.Id == user.Id),
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

            _hubContext.Clients.All.Alert($"Игра {viewModel.Name} добавлена на сайт");

            _gameStoreRepository.Add(dataGame);

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

        [IsAuthenticated]
        public IActionResult Profile()
        {
            var userId = _authService.GetUserId();

            var user = _gameStoreRepository.GetInfoAboutUser(userId);

            var viewModel = new Models.GameStore.ProfileViewModel
            {
                UserName = user.Login,
                AvatarUrl = user.AvatarUrl,
                Balance = user.Coins,
                Role = Enum.GetName(typeof(Role), user.Role)
            };



            return View(viewModel);
        }
        [IsAuthenticated]
        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public IActionResult UpdateAvatar(IFormFile avatar)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var userId = _authService.GetUserId()!.Value;
            var avatarFileName = $"avatar-{userId}.jpg";

            var path = Path.Combine(webRootPath, "images", "avatars", avatarFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                avatar
                    .CopyToAsync(fileStream)
                    .Wait();
            }

            var avatarUrl = $"/images/avatars/{avatarFileName}";
            _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

            return RedirectToAction("Profile");
        }
        public IActionResult DeleteAvatar(string oldAvatar, int userId)
        {
            var fileName = Path.GetFileName(oldAvatar);
            var webRootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, "images", "avatars", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден.");
            }
            System.IO.File.Delete(filePath);

            var avatarUrl = $"/images/avatars/default.jpg";
            _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

            return RedirectToAction("Profile");
        }
        public IActionResult PopularGame()
        {
            var viewModels = _gameStoreRepository
                .GetMostPopularGames()
                .Select(db => new PopularGamesViewModel
                {
                    GameTitle = db.GameTitle,
                    PlayersCount = db.PlayersCount,
                    AverageAge = db.AverageAge,
                })
                .ToList();
            return View(viewModels);
        }
    }
}
