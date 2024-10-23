using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IMoviePosterRepository
    {
        void Add(IMovieData data);

        void Delete(IMovieData data);

        List<IMovieData> GetAllInCount(int count);

        List<IMovieData> GetAll();

        IMovieData? Get(int id); 

        bool Any();
    }
}
