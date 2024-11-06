using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IBrandRepositoryReal : IBrandRepository<BrandData>
    {
        
    }

    public class BrandRepository : BaseRepository<BrandData>, IBrandRepositoryReal
    {
        public BrandRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<BrandData> GetAllWithCoffe()
        {
            return _dbSet
                .Include(x => x.Coffe)
                .ToList();
        }

        public void LinkCoffe(int brandId, int coffeId)
        {
            var coffe = _webDbContext.Coffe.First(x => x.Id == coffeId);
            var brand = _dbSet.First(x => x.Id == brandId);

            brand.Coffe.Add(coffe);

            _webDbContext.SaveChanges();
        }
    }
}
