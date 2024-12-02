using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IMagazinRepositoryReal : IMagazinRepository<MagazinData>
    {
        void Create(MagazinData dataMagazin, int currentUserId);
        IEnumerable<MagazinData> GetMyMagazinsICreated(int? userId);
        void UpdateName(int id, string newName);
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

        public IEnumerable<MagazinData> GetMyMagazinsICreated(int? userId)
        {
            var listMagazins = _dbSet.Where(x => x.Creator.Id == userId);
            return listMagazins;
        }

        public void UpdateName(int id, string newName)
        {
            var magazin = _dbSet.FirstOrDefault(x => x.Id == id);
            magazin.Name = newName;

            _webDbContext.SaveChanges();
        }
    }
}
