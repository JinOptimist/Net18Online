using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IMagazinRepositoryReal : IMagazinRepository<MagazinData>
    {
        void Create(MagazinData dataMagazin, int currentUserId);
    }
    public class MagazinRepository : BaseRepository<MagazinData>, IMagazinRepositoryReal
    {
        public MagazinRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(MagazinData dataMagazin, int currentUserId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);

            dataMagazin.Creator = creator;

            Add(dataMagazin);
        }
    }
}
