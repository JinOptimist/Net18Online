using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Everything.Data.DataLayerModels;

namespace Everything.Data.Repositories
{
    public interface IGameStoreRepositoryReal : IGameStoreRepository<GameData>
    {
        IEnumerable<GameData> AllBuyersGames();
        IEnumerable<GameData> GetWithoutOwner();
        IEnumerable<GameWithInfoAboutAuthor> GetGameWithBuyer(int buyerId);
        void LinkGame(int buyerId, int gameId);
        bool HasSimilarName(string name);
    }
    public class GameStoreRepository : BaseRepository<GameData>, IGameStoreRepositoryReal
    {
        public GameStoreRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<GameData> AllBuyersGames()
        {
            return _dbSet.Include(x => x.Buyer);
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
        public void LinkGame(int buyerId, int gameId)
        {
            var game = _webDbContext.Games.First(x => x.Id == gameId);
            var buyer = _dbSet.First(x => x.Id == buyerId);

            buyer.Buyer?.Games.Add(game);
            _webDbContext.SaveChanges();
        }

        public IEnumerable<GameWithInfoAboutAuthor> GetGameWithBuyer(int buyerId)
        {
            var games = _dbSet
                .Where(user => user
                .Buyer
                .Games
                .Any(game => game.Buyer != null
                && game.Buyer.Id == buyerId))
                .Select(x => new GameWithInfoAboutAuthor
                { 
                    Name = x.NameGame,
                    ImageSrc = x.ImageSrc,
                }).ToList();

            return game;
        }
    }


}

