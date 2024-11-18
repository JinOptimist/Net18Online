using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IMoviePosterRepositoryReal : IMoviePosterRepository<MovieData>
    {
        IEnumerable<MovieData> GetAllWithoutDirector();
    }

    public class MoviePosterRepository : BaseRepository<MovieData>, IMoviePosterRepositoryReal
    {

        public MoviePosterRepository(WebDbContext webDbContext) : base(webDbContext)
        {
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
