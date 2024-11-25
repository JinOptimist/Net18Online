using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface ICakeRepositoryReal : ICakeRepository<CakeData>
    {
        void Create(CakeData dataCake, int currentUserId);
        void Link(int cakeId, int magazinId);
        bool IsUrlUniq(string url);
    }
    public class CakeRepository : BaseRepository<CakeData>, ICakeRepositoryReal
    {
        public CakeRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(CakeData dataCake, int currentUserId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);

            dataCake.Creator = creator;

            Add(dataCake);
        }

        public bool IsUrlUniq(string url)
        {
            return !_dbSet.Any(x => x.ImageSrc == url);
        }

        public void Link(int cakeId, int magazinId)
        {
            var cake = _webDbContext.Cakes
                .Include(c => c.Magazins)
                .FirstOrDefault(x => x.Id == cakeId);
            var magazin = _webDbContext.Magazines
                .FirstOrDefault(x => x.Id == magazinId);

            cake.Magazins.Add(magazin);
            _webDbContext.SaveChanges();
        }

        public void UpdateDescription(int id, string newDescription)
        {
            var cakeDescription = _webDbContext.Cakes.First(x => x.Id == id);

            cakeDescription.Description = newDescription;

            _webDbContext.SaveChanges();
        }
    }
}
