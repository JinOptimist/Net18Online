using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IMangaRepository<T> : IBaseRepository<T>
        where T : IMangaData
    {
    }
}
