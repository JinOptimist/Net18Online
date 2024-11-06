using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Services
{
    public class GeneratorMoviePosters
    {
        private IMoviePosterRepositoryReal _moviePosterRepository;

        public GeneratorMoviePosters(IMoviePosterRepositoryReal moviePosterRepository)
        {
            _moviePosterRepository = moviePosterRepository;
        }

        public void Generate()
        {

        }
    }
}
