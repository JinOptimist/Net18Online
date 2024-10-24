using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class MoviePosterRepository : IMoviePosterRepository
    {
        private List<IMovieData> movies = new List<IMovieData>();

        public void Add(IMovieData data)
        {
            data.Id = movies.Any()
                ? movies.Max(x => x.Id) + 1
                : 1;

            movies.Add(data);
        }

        public void Delete(IMovieData data)
        {
            movies.Remove(data);
        }

        public List<IMovieData> GetAllInCount(int count)
        {
            movies.Take(count).ToList();
            return movies;
        }

        public List<IMovieData> GetAll()
        {
            return movies;
        }

        public IMovieData? Get(int id)
        {
            return movies.FirstOrDefault(x => x.Id == id);
        }

        public bool Any()
        {
            return movies.Any();
        }
    }
}
