using Everything.Data.DataLayerModels;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface ICakeRepositoryReal : ICakeRepository<CakeData>
    {
        IEnumerable<CakeData> GetAllWithCakesAndMagazins();
        Pagginator<CakeData> GetAllWithCakesAndMagazins(int page, int perPage);
        void Create(CakeData dataCake, int currentUserId);
        void Link(int cakeId, int magazinId);
        bool IsUrlUniq(string url);
        IEnumerable<CakeData> GetMyCakesICreated(int? userId);
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

        public Pagginator<CakeData> GetAllWithCakesAndMagazins(int page, int perPage)
        {
            var items = WithMagazins().AsQueryable();

            var data = new Pagginator<CakeData>();
            data.TotalRecords = items.Count();
            data.Items = items.Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            return data;
        }

        public IEnumerable<CakeData> GetAllWithCakesAndMagazins()
        {
            return WithMagazins()
                .ToList();
        }

        public IEnumerable<CakeData> GetMyCakesICreated(int? userId)
        {
            var listCakes = _dbSet.Where(x => x.Creator.Id == userId);
            return listCakes;
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

        public void UpdateImage(int id, string newImage)
        {
            var cakeImage = _webDbContext.Cakes.First(x => x.Id == id);

            cakeImage.ImageSrc = newImage;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var cakeImage = _webDbContext.Cakes.First(x => x.Id == id);

            cakeImage.Name = newName;

            _webDbContext.SaveChanges();
        }

        public void UpdatePrice(int id, decimal newPrice)
        {
            var cakeImage = _webDbContext.Cakes.First(x => x.Id == id);

            cakeImage.Price = newPrice;

            _webDbContext.SaveChanges();
        }

        private IQueryable<CakeData> WithMagazins()
        {
            return _dbSet
                .Include(x => x.Magazins);
        }
    }
}
