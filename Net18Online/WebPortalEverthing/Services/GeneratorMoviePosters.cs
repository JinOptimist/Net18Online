using Everything.Data.Interface.Repositories;

namespace WebPortalEverthing.Services
{
    public class GeneratorMoviePosters
    {
        private IMoviePosterRepository _moviePosterRepository;

        public GeneratorMoviePosters(IMoviePosterRepository moviePosterRepository)
        {
            _moviePosterRepository = moviePosterRepository;
        }

        public void Generate()
        {

        }
    }
}
