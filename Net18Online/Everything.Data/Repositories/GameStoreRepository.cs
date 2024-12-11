using Everything.Data.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Everything.Data.DataLayerModels;
using Everything.Data.Models.SqlRawModels;

namespace Everything.Data.Repositories
{
    public interface IGameStoreRepositoryReal : IGameStoreRepository<GameData>
    {
        IEnumerable<GameData> AllBuyersGames();
        IEnumerable<GameData> GetWithoutOwner();
        IEnumerable<GameWithInfoAboutAuthor> GetGameWithBuyer(int buyerId);
        void LinkGame(int buyerId, int gameId);
        bool HasSimilarName(string name);
        UserData GetInfoAboutUser(int? userId);
        IEnumerable<MostPopularGames> GetMostPopularGames();
        IEnumerable<GameData> GetAllWithStudio();
        bool LikeGame(int gameId, int userId);
        bool DislikeGame(int gameId, int userId);
    }
    public class GameStoreRepository : BaseRepository<GameData>, IGameStoreRepositoryReal
    {
        public GameStoreRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<GameData> AllBuyersGames()
        {
            return _dbSet.Include(x => x.Buyers);
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
            var buyer = _webDbContext.Users.Include(u => u.Games).FirstOrDefault(x => x.Id == buyerId);
            if (buyer == null)
                throw new InvalidOperationException($"Buyer with ID {buyerId} not found.");

            var game = _webDbContext.Games.FirstOrDefault(x => x.Id == gameId);
            if (game == null)
                throw new InvalidOperationException($"Game with ID {gameId} not found.");

            if (buyer.Games.Contains(game))
            {
                throw new InvalidOperationException($"The buyer {buyerId} already has the game {gameId}");
            }
            buyer.Games.Add(game);
            _webDbContext.SaveChanges();
        }

        public IEnumerable<GameWithInfoAboutAuthor> GetGameWithBuyer(int buyerId)
        {
            var games = _dbSet
                .Where(game => game.Buyers.Any(buyer => buyer.Id == buyerId))
                .Select(x => new GameWithInfoAboutAuthor
                {
                    Name = x.NameGame,
                    ImageSrc = x.ImageSrc,
                }).ToList();

            return games;
        }

        public UserData GetInfoAboutUser(int? userId)
        {
            var user = _webDbContext.Users.FirstOrDefault(x => x.Id == userId);

            return user;

        }
        public IEnumerable<MostPopularGames> GetMostPopularGames()
        {
            var sql = @"
SELECT 
    g.NameGame AS GameTitle,          
    COUNT(udgd.BuyersId) AS PlayersCount, 
    AVG(u.Age) AS AverageAge
FROM 
    GameDataUserData udgd
JOIN 
    Games g ON udgd.GamesId = g.Id   
JOIN
    Users u ON udgd.BuyersId = u.Id
GROUP BY 
    g.NameGame                         
ORDER BY 
    PlayersCount DESC, g.NameGame; ";
            var result = _webDbContext
                .Database
                .SqlQueryRaw<MostPopularGames>(sql)
                .ToList();

            return result;
        }

        public IEnumerable<GameData> GetAllWithStudio()
        {
            var result = _webDbContext.Games
                .Include(game => game.UsersWhoLikedGame)
                .Include(game => game.UsersWhoDislikedGame)
                .Include(game => game.Studios) 
                .ToList();

            return result;
        }

        public bool LikeGame(int gameId, int userId)
        {
            var game = _dbSet
                .Include(x => x.UsersWhoLikedGame)
                .First(x => x.Id == gameId);

            var isUserAlreadyLikeTheGame = _dbSet
                .Any(g => g.Id == userId
                && g.UsersWhoLikedGame.Any(u => u.Id == userId));
            var user = _webDbContext.Users.First(x => x.Id == userId);

            if (isUserAlreadyLikeTheGame)
            {                
                game.UsersWhoLikedGame.Remove(user);
                _webDbContext.SaveChanges();
                return false;
            }
            game.UsersWhoLikedGame.Add(user);
            _webDbContext.SaveChanges();
            return true;

        }

        public bool DislikeGame(int gameId, int userId)
        {
            var game = _dbSet
                .Include(x => x.UsersWhoDislikedGame)
                .First(x => x.Id == gameId);

            var isUserAlreadyDislikeTheGame = _dbSet
                .Any(g => g.Id == userId
                && g.UsersWhoDislikedGame.Any(u => u.Id == userId));
            var user = _webDbContext.Users.First(x => x.Id == userId);
            if (isUserAlreadyDislikeTheGame)
            {                
                game.UsersWhoDislikedGame.Remove(user);
                _webDbContext.SaveChanges();
                return false;
            }
            game.UsersWhoDislikedGame.Add(user);
            _webDbContext.SaveChanges();
            return true;
        }
    }

}

