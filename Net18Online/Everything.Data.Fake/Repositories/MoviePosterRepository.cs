using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class MoviePosterRepository : BaseRepository<IMovieData>, IMoviePosterRepository
    {
        public List<IMovieData> GetAllInCount(int count)
        {
            _entyties.Take(count).ToList();
            return _entyties;
        }
    }
}
