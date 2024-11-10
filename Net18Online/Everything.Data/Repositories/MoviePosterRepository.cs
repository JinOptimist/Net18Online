using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IMoviePosterRepositoryReal : IMoviePosterRepository<MovieData>
    {
    }

    public class MoviePosterRepository : IMoviePosterRepositoryReal
    {
        private WebDbContext _webDbContext;

        public MoviePosterRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(MovieData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Movies.Any();
        }

        public void Delete(MovieData data)
        {
            _webDbContext.Movies.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public MovieData? Get(int id)
        {
            return _webDbContext.Movies.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<MovieData> GetAll()
        {
            return GetFinilizeMovies().ToList();
        }

        public IEnumerable<MovieData> GetAllInCount(int count)
        {
            return GetFinilizeMovies()
                .Take(count)
                .ToList();
        }

        public void UpdateImage(int id, string url)
        {
            var movie = _webDbContext.Movies.First(x => x.Id == id);

            movie.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var movie = _webDbContext.Movies.First(x => x.Id == id);

            movie.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<MovieData> GetFinilizeMovies()
        {
            return _webDbContext
                .Movies
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}
