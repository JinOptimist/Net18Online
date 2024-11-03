using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface IGameStoreRepositoryReal : IGameStoreRepository<GameData>
    {
    }
    public class GameStoreRepository : IGameStoreRepositoryReal
    {
        private WebDbContext _webDbContext;

        public GameStoreRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(GameData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Girls.Any();
        }

        public void Delete(GameData data)
        {
            _webDbContext.Games.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public GameData? Get(int id)
        {
            return _webDbContext.Games.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GameData> GetAll()
        {
            return GetFinilizeGame().ToList();
        }

        public void UpdateImage(int id, string url)
        {
            var game = _webDbContext.Games.First(x => x.Id == id);

            game.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var game = _webDbContext.Games.First(x => x.Id == id);

            game.NameGame = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<GameData> GetFinilizeGame()
        {
            return _webDbContext
                .Games
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc))
                .OrderBy(x => x.Id);
        }
    }


}
