using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
	public interface IKeyCoffeShopRepository : ICoffeShopRepository<CoffeData>
	{
	}

	public class CoffeShopRepository : BaseRepository<CoffeData>, IKeyCoffeShopRepository
	{
		public CoffeShopRepository(WebDbContext webDbContext) : base(webDbContext)
		{
		}

        public IEnumerable<CoffeData> GetCoffeByName(string name)
		{
			return _dbSet
				.Where(x => x.Coffe == name)
				.ToList();
		}

		public void UpdateCoffeName(int id, string name)
		{

			var item = _dbSet.First(x => x.Id == id);

			item.Coffe = name;

			_webDbContext.SaveChanges();
		}

		public void UpdateCost(int id, decimal cost)
		{

			var item = _dbSet.First(x => x.Id == id);

			item.Cost = cost;

			_webDbContext.SaveChanges();
		}

		public void UpdateImage(int id, string url)
		{

			var item = _dbSet.First(x => x.Id == id);

			item.Url = url;

			_webDbContext.SaveChanges();
		}

		private IQueryable<CoffeData> SerializeObject()
		{
			return _dbSet
				.Where(x => !string.IsNullOrEmpty(x.Url));
		}
	}
}