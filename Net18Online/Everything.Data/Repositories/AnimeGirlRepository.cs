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
        GirlData GetWithCreatorsAndManga(int id);
        bool HasSimilarName(string name);
        bool IsNameUniq(string name);
        IEnumerable<GirlData> GetAllByAuthorId(int userId);
        IEnumerable<GirlsWithDuplicateInfo> GetGirlsWithDuplicateInfo();
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
                .ToList();
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
    }
}
