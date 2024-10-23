using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeCatalogRepository
    {
        void Add(IAnimeCatalogData data);

        void Delete(IAnimeCatalogData data);

        List<IAnimeCatalogData> GetAll();

        IAnimeCatalogData? Get(int id);

        bool Any();
    }
}
