using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface IGameStudiosRepositoryReal : IGameStudiosRepository<GameStudiosData>
    {
        IEnumerable<GameStudiosData> GetAllWithGame();
        void LinkGame(int studioId, int gameId);
    }

    public class GameStudiosRepository : BaseRepository<GameStudiosData>, IGameStudiosRepositoryReal
    {
        public GameStudiosRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<GameStudiosData> GetAllWithGame()
        {
            return _dbSet
                .Include(x=>x.Games)
                .ToList();
        }

        public void LinkGame(int studioId, int gameId)
        {
            var game = _webDbContext.Games.First(x => x.Id == gameId);
            var studio = _dbSet.First(x=> x.Id == studioId);

            studio.Games.Add(game);
            _webDbContext.SaveChanges();
        }
    }
}
