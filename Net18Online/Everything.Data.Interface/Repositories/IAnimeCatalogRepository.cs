using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeCatalogRepository<T> : IBaseRepository<T>
        where T : IAnimeCatalogData
    {
    }
}
