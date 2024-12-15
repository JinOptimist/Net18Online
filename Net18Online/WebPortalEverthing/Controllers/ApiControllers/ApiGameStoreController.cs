using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Hubs;
using WebPortalEverthing.Models.GameStore;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiGameStoreController : ControllerBase
    {
        public IGameStoreRepositoryReal _gameStoreRepository;
        public AuthService _authService;
        private IHubContext<GameAlertHub, IGameAlertHub> _hubContext;

        public ApiGameStoreController(IGameStoreRepositoryReal gameStoreRepository, AuthService authService, IHubContext<GameAlertHub, IGameAlertHub> hubContext)
        {
            _gameStoreRepository = gameStoreRepository;
            _authService = authService;
            _hubContext = hubContext;
        }
        public bool UpdateName(string newName, int id)
        {
            if (newName.Contains("test"))
            {
                return false;
            }
            _gameStoreRepository.UpdateName(id, newName);
            return true;
        }
        public bool Remove(int id)
        {
            _gameStoreRepository.Delete(id);
            return true;
        }

        public ShopViewModel Add(ApiGameAddViewModel data)
        {
            var dataGame = new GameData
            {
                NameGame = data.name,
                ImageSrc = data.url,
                Cost = (int)data.cost,
                //Tags = new()
            };
                        
            var id = _gameStoreRepository.Add(dataGame);

            var dbGame = _gameStoreRepository.Get(id);

            var viewModel = new ShopViewModel
            {
                Id = dbGame.Id,
                NameGame = dbGame.NameGame,
                ImageSrc = dbGame.ImageSrc,
                Cost = dbGame.Cost,
                Studios = dbGame.Studios?.Name
            };

            _hubContext.Clients.All.Alert($"Игра {viewModel.NameGame} добавлена на сайт");

            return viewModel;
        }
        [Authorize]
        public bool Like(int gameId)
        {
            var userId = _authService.GetUserId()!.Value;            

            return _gameStoreRepository.LikeGame(gameId, userId);
        }
        [Authorize]
        public bool Dislike(int gameId)
        {
            var userId = _authService.GetUserId()!.Value;

            return _gameStoreRepository.DislikeGame(gameId, userId);
        }
    }
}
