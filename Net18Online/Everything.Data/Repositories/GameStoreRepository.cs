using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface IGameStoreRepositoryReal : IGameStoreRepository<GameData>
    {
        IEnumerable<GameData> GetWithoutOwner();
        bool HasSimilarName(string name);
    }
    public class GameStoreRepository : BaseRepository<GameData>, IGameStoreRepositoryReal
    {
        public GameStoreRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<GameData> GetWithoutOwner()
        {
            return _dbSet
                .Where(x => x.Studios == null).ToList();
        }


        public bool HasSimilarName(string name)
        {
            return _dbSet.Any(x => x.NameGame.StartsWith(name) || name.StartsWith(x.NameGame));
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

