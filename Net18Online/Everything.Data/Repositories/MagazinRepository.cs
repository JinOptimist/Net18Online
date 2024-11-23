using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IMagazinRepositoryReal : IMagazinRepository<MagazinData>
    {
    }
    public class MagazinRepository : BaseRepository<MagazinData>, IMagazinRepositoryReal
    {
        public MagazinRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }
    }
}
