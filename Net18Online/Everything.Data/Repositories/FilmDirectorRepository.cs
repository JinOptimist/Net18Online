using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IFilmDirectorRepositoryReal : IFilmDirectorRepository<FilmDirectorData>
    {
        IEnumerable<FilmDirectorData> GetAllWithFilms();
        void LinkMovie(int filmDirectorId, int movieId);
    }
    public class FilmDirectorRepository : BaseRepository<FilmDirectorData>, IFilmDirectorRepositoryReal
    {
        public FilmDirectorRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<FilmDirectorData> GetAllWithFilms()
        {
            return _dbSet
                .Include(x => x.Films)
                .ToList();
        }

        public void LinkMovie(int filmDirectorId, int movieId)
        {
            var movie = _webDbContext.Movies.First(x => x.Id == movieId);
            var filmDirector = _dbSet.First(x => x.Id == filmDirectorId);

            filmDirector.Films.Add(movie);

            _webDbContext.SaveChanges();
        }
    }
}
