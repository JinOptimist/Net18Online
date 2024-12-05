using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IMoviePosterRepositoryReal : IMoviePosterRepository<MovieData>
    {
        void Create(MovieData dataMovie, int currentUserId, int filmDirectorId);
        bool HasSimilarUrl(string url);
        IEnumerable<MovieData> GetAllWithoutDirector();
        IEnumerable<MovieData> GetAllWithСreatorsAndFilmDirectors();
        IEnumerable<MovieData> GetAllByCreatorId(int userId);
        IEnumerable<MoviePosterCountElementInDbAndFirstAndLastElement> GetCountOfElementsInDbAndShowFirstAndLastPosterInfo();
    }

    public class MoviePosterRepository : BaseRepository<MovieData>, IMoviePosterRepositoryReal
    {

        public MoviePosterRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(MovieData dataMovie, int currentUserId, int filmDirectorId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);
            var filmDirector = _webDbContext.FilmDirectors.First(x => x.Id == filmDirectorId);

            dataMovie.Creator = creator;
            dataMovie.FilmDirector = filmDirector;

            Add(dataMovie);
        }

        public IEnumerable<MovieData> GetAllByCreatorId(int userId)
        {
            return _dbSet
                .Where(x => x.Creator != null 
                    && x.Creator.Id == userId)
                .ToList();
        }

        public IEnumerable<MovieData> GetAllInCount(int count)
        {
            return GetFinilizeMovies()
                .Take(count)
                .ToList();
        }

        public IEnumerable<MovieData> GetAllWithoutDirector()
        {
            return _dbSet
                .Where(x => x.FilmDirector == null)
                .ToList();
        }
        public IEnumerable<MovieData> GetAllWithСreatorsAndFilmDirectors()
        {
            return _dbSet
                .Include(x => x.Creator)
                .Include(x => x.FilmDirector)
                .ToList();
        }

        public IEnumerable<MoviePosterCountElementInDbAndFirstAndLastElement> GetCountOfElementsInDbAndShowFirstAndLastPosterInfo()
        {
            var sql = @"
WITH RankedItems AS (
    SELECT 
        Name,
        ImageSrc,
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
        COUNT(*) OVER () AS [Count] -- Общее количество элементов
    FROM 
        Movies
)

SELECT 
    [Count],
    Name,
    ImageSrc
FROM 
    RankedItems
WHERE 
    RowNum = 1 -- Первый элемент

UNION ALL

SELECT 
    [Count],
    Name,
    ImageSrc
FROM 
    RankedItems
WHERE 
    RowNum = (SELECT COUNT(*) FROM RankedItems); -- Последний элемент
";

            var result = _webDbContext
                .Database
                .SqlQueryRaw<MoviePosterCountElementInDbAndFirstAndLastElement>(sql)
                .ToList();

            return result;
        }

        public bool HasSimilarUrl(string url)
        {
            return _dbSet.Any(x => x.ImageSrc == url);
        }

        public void UpdateImage(int id, string url)
        {
            var movie = _dbSet.First(x => x.Id == id);

            movie.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var movie = _dbSet.First(x => x.Id == id);

            movie.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<MovieData> GetFinilizeMovies()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}
