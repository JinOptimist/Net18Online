using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IMoviePosterRepository<T> : IBaseRepository<T>
        where T : IMovieData
    {
        IEnumerable<T> GetAllInCount(int count);
        void UpdateName(int id, string newName);

        void UpdateImage(int id, string url);
    }
}
