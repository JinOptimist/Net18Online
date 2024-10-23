using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class CoffeShopRepository : ICoffeShopRepository
    {
        private List<ICoffeData> coffe = new List<ICoffeData>();

        public void Add(ICoffeData data)
        {
            data.Id = coffe.Any()
                ? coffe.Max(x => x.Id) + 1 
                : 1;

            coffe.Add(data);
        }

        public void Delete(ICoffeData data)
        {
            coffe.Remove(data);
        }

        public List<ICoffeData> GetAll()
        {
            return coffe;
        }

        public ICoffeData? Get(int id)
        {
            return coffe.FirstOrDefault(x => x.Id == id);
        }

        public bool Any()
        {
            return coffe.Any();
        }
    }
}