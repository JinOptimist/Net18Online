using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
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
