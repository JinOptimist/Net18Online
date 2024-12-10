using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.GameStore;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiGameStoreController : ControllerBase
    {
        public IGameStoreRepositoryReal _gameStoreRepository;

        public ApiGameStoreController(IGameStoreRepositoryReal gameStoreRepository)
        {
            _gameStoreRepository = gameStoreRepository;
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

            return viewModel;
        }
    }
}
