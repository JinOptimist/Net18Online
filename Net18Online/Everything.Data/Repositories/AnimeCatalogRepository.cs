using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IAnimeCatalogRepositoryReal : IAnimeCatalogRepository<AnimeData>
    {
        IEnumerable<AnimeData> GetWithoutDescription();
    }

    public class AnimeCatalogRepository : BaseRepository<AnimeData>, IAnimeCatalogRepositoryReal
    {
        public AnimeCatalogRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<AnimeData> GetWithoutDescription()
        {
            return _dbSet
                .Where(x => x.Description == null)
                .ToList();
        }
    }
}
