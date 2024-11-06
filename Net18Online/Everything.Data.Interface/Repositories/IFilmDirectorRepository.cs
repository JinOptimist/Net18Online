using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IFilmDirectorRepository<T> : IBaseRepository<T>
        where T : IFilmDirectorData
    {
    }
}
