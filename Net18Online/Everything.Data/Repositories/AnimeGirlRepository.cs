using Enums;
using Enums.Girls;
using Everything.Data.DataLayerModels;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IAnimeGirlRepositoryReal : IAnimeGirlRepository<GirlData>
    {
        int Create(GirlData dataGirl, int currentUserId, int mangaId);
        IEnumerable<GirlData> GetWithoutManga();
        IEnumerable<GirlData> GetAllWithCreatorsAndManga();
        Pagginator<GirlData> GetAllWithCreatorsAndManga(int page, int perPage,
            string fieldNameForSort, 
            OrderDirection orderDirection);
        GirlData GetWithCreatorsAndManga(int id);
        bool HasSimilarName(string name);
        bool IsNameUniq(string name);
        IEnumerable<GirlData> GetAllByAuthorId(int userId);
        IEnumerable<GirlsWithDuplicateInfo> GetGirlsWithDuplicateInfo();

        /// <summary>
        /// Return true if girl wasn't like. And now she is
        /// Return false if girl was liked but not is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool LikeGirl(int girlId, int userId);
    }

    public class AnimeGirlRepository : BaseRepository<GirlData>, IAnimeGirlRepositoryReal
    {
        public AnimeGirlRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public int Create(GirlData dataGirl, int currentUserId, int mangaId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);
            var manga = _webDbContext.Mangas.First(x => x.Id == mangaId);

            dataGirl.Creator = creator;
            dataGirl.Manga = manga;

            return Add(dataGirl);
        }

        public IEnumerable<GirlData> GetMostPopular()
        {
            return GetFinilizeGirl()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public IEnumerable<GirlData> GetWithoutManga()
        {
            return _dbSet
                .Where(x => x.Manga == null)
                .ToList();
        }

        public IEnumerable<GirlData> GetAllWithCreatorsAndManga()
        {
            return WithCreatorsAndManga()
                .Include(x => x.UsersWhoLikeIt)
                .ToList();
        }

        public Pagginator<GirlData> GetAllWithCreatorsAndManga(
            int page,
            int perPage,
            string fieldNameForSort,
            OrderDirection orderDirection)
        {
            var items = WithCreatorsAndManga()
                .Include(x => x.UsersWhoLikeIt)
                .AsQueryable();

            items = SortAndGetAll(items, fieldNameForSort);

            if (orderDirection == OrderDirection.Desc)
            {
                items = items.Reverse();
            }

            var data = new Pagginator<GirlData>();
            data.TotalRecords = items.Count();
            data.Items = items.Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
            return data;
        }

        public GirlData GetWithCreatorsAndManga(int id)
        {
            return WithCreatorsAndManga()
                .First(x => x.Id == id);
        }

        public bool HasSimilarName(string name)
        {
            return _dbSet.Any(x => x.Name.StartsWith(name) || name.StartsWith(x.Name));
        }

        public bool IsNameUniq(string name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }

        public void UpdateImage(int id, string url)
        {
            var girl = _dbSet.First(x => x.Id == id);

            girl.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var girl = _dbSet.First(x => x.Id == id);

            girl.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<GirlData> GetFinilizeGirl()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }

        public IEnumerable<GirlData> GetAllByAuthorId(int userId)
        {

            return _dbSet
                .Where(x => x.Creator != null
                    && x.Creator.Id == userId)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public IEnumerable<GirlsWithDuplicateInfo> GetGirlsWithDuplicateInfo()
        {
            var sql = @"
SELECT G.Id
	,G.Name
	,G.ImageSrc
	,CASE WHEN OriginId IS NULL THEN 'Uniq' ELSE 'NotUniq' END as UniqStatus
	,CASE WHEN OriginId IS NULL OR OriginId = G.Id THEN 'Original' ELSE 'Duplicate' END as DuplicateStatus
	,CASE WHEN OriginId IS NOT NULL AND OriginId <> G.Id THEN OriginId ELSE NULL END as OriginId
	,CASE WHEN OriginId IS NOT NULL AND OriginId <> G.Id THEN G2.Name ELSE NULL END as OriginName
FROM Girls G
	LEFT JOIN (
		SELECT MIN(Id) OriginId, ImageSrc, COUNT(*) CountOfDuplication
		FROM Girls
		GROUP BY ImageSrc
		HAVING COUNT(*) > 1
	) DI ON G.ImageSrc = DI.ImageSrc
	LEFT JOIN Girls G2 ON DI.OriginId = G2.Id";

            var result = _webDbContext
                .Database
                .SqlQueryRaw<GirlsWithDuplicateInfo>(sql)
                .ToList();

            return result;
        }

        private IQueryable<GirlData> WithCreatorsAndManga()
        {
            return _dbSet
                .Include(x => x.Creator)
                .Include(x => x.Manga);
        }

        public bool LikeGirl(int girlId, int userId)
        {
            var girl = _dbSet
                .Include(x => x.UsersWhoLikeIt)
                .First(x => x.Id == girlId);
            var user = _webDbContext.Users.First(x => x.Id == userId);

            var isUserAlreadyLikeTheGirl = girl
                .UsersWhoLikeIt
                .Any(u => u.Id == userId);

            if (isUserAlreadyLikeTheGirl)
            {
                girl.UsersWhoLikeIt.Remove(user);
                _webDbContext.SaveChanges();
                return false;
            }

            girl.UsersWhoLikeIt.Add(user);
            _webDbContext.SaveChanges();
            return true;
        }
    }
}
