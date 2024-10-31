using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IKeyCoffeShopRepository : ICoffeShopRepository<CoffeData> { }

    public class CoffeShopRepository : IKeyCoffeShopRepository
    {
        private WebDbContext _webDbContext;

        public CoffeShopRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(CoffeData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Coffe.Any();
        }

        public void Delete(CoffeData data)
        {
            _webDbContext.Coffe.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

        public CoffeData? Get(int id)
        {
            return _webDbContext
                .Coffe
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CoffeData> GetAll()
        {
            return SerializeObject().ToList();
        }

        public IQueryable<CoffeData> SerializeObject()
        {
            return _webDbContext
                .Coffe
                .Where(x => !string.IsNullOrEmpty(x.Url));
        }

        public void UpdateBrand(int id, string brand)
        {
            var item = _webDbContext
                .Coffe
                .FirstOrDefault(x => x.Id == id);

            item.Brand = brand;

            _webDbContext.SaveChanges();
        }

        public void UpdateCoffeName(int id, string name)
        {

            var item = _webDbContext
                .Coffe
                .FirstOrDefault(x => x.Id == id);

            item.Coffe = name;

            _webDbContext.SaveChanges();
        }

        public void UpdateCost(int id, decimal cost)
        {

            var item = _webDbContext
                .Coffe
                .FirstOrDefault(x => x.Id == id);

            item.Cost = cost;

            _webDbContext.SaveChanges();
        }

        public void UpdateImage(int id, string url)
        {

            var item = _webDbContext
                .Coffe
                .FirstOrDefault(x => x.Id == id);

            item.Url = url;

            _webDbContext.SaveChanges();
        }
    }
}