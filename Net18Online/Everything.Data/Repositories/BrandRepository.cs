using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IBrandRepositoryReal : IBrandRepository<BrandData>
    {
        public IEnumerable<UniqBrandNamesInfo> GetBrandsNamesWithUniqStatusInfo();
        bool IsBrandUniq(string brand);

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


        public bool IsBrandUniq(string brand)
        {
            return !_dbSet.Any(x => x.Name == brand);
        }

        public IEnumerable<UniqBrandNamesInfo> GetBrandsNamesWithUniqStatusInfo()
        {
            var sql = @"
SELECT 
    B.Id,
    B.Name AS BrandName,
    CASE WHEN DI.OriginId IS NULL THEN 'Uniq' ELSE 'NotUniq' END AS UniqStatus,
    CASE WHEN DI.OriginId IS NULL OR DI.OriginId = B.Id THEN 'Original' ELSE 'Duplicate' END AS DuplicateStatus
FROM Brands B
LEFT JOIN (
    SELECT MIN(BrandId) AS OriginId, COUNT(*) AS CountOfDuplication
    FROM Coffe
    GROUP BY BrandId
    HAVING COUNT(*) > 1
) DI ON B.Id = DI.OriginId;
";
            var result = _webDbContext
                .Database
                .SqlQueryRaw<UniqBrandNamesInfo>(sql)
                .ToList();

            return result;
        }
    }
}
