using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IMoviePosterRepository : IBaseRepository<IMovieData>
    {
        List<IMovieData> GetAllInCount(int count);
    }
}
