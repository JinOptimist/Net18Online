using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.GameStore;
using WebPortalEverthing.Models.GameStudios;

namespace WebPortalEverthing.Controllers
{
    public class GameStudiosController : Controller
    {
        private IGameStudiosRepositoryReal _gameStudiosRepository;
        private IGameStoreRepositoryReal _gameStoreRepository;

        public GameStudiosController(IGameStudiosRepositoryReal gameStudiosRepository, IGameStoreRepositoryReal gameStoreRepository)
        {
            _gameStudiosRepository = gameStudiosRepository;
            _gameStoreRepository = gameStoreRepository;
        }

        public IActionResult Index()
        {
            var studiosFromDb = _gameStudiosRepository
                .GetAllWithGame()
                .Select(x => new StudiosShortInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    GameNames = x.Games.Select(gameData => 
                    new GameShortInfoViewModel { 
                        Id = gameData.Id,
                        Name = gameData.NameGame,
                    }).ToList()
                })
                .ToList();

            var gameFromDb = _gameStoreRepository
                .GetWithoutOwner()
                .Select(x => new GameShortInfoViewModel
                {
                    Id = x.Id,
                    Name = x.NameGame,
                })
                .ToList();

            var viewModel = new IndexStudiosViewModel
            {
                Studios = studiosFromDb,
                Games = gameFromDb
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateStudiosViewModel viewModel)
        {
            var studios = new GameStudiosData
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
            };

            _gameStudiosRepository.Add(studios);
            return RedirectToAction("IndexLoadVolume");
        }
        [HttpPost]
        public IActionResult LinkGameAndStudio(int studioId, int gameId)
        {
            _gameStudiosRepository.LinkGame(studioId, gameId);
            return RedirectToAction("IndexLoadVolume");
        }
    }
}
