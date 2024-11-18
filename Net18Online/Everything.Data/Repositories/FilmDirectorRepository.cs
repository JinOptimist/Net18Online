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
    public interface IFilmDirectorRepositoryReal : IFilmDirectorRepository<FilmDirectorData>
    {
        IEnumerable<FilmDirectorData> GetAllWithMovies();
        void LinkMovie(int filmDirectorId, int movieId);
    }

    public class FilmDirectorRepository : BaseRepository<FilmDirectorData>, IFilmDirectorRepositoryReal
    {
        public FilmDirectorRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void LinkMovie(int filmDirectorId, int movieId)
        {
            var movie = _webDbContext.Movies.First(x => x.Id == movieId);
            var filmDirector = _dbSet.First(x => x.Id == filmDirectorId);

            filmDirector.Movies.Add(movie);

            _webDbContext.SaveChanges();
        }

        public IEnumerable<FilmDirectorData> GetAllWithMovies()
        {
            return _dbSet
                .Include(x => x.Movies)
                .ToList();
        }
    }
}
