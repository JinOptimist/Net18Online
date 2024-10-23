using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICoffeShopRepository
    {
        void Add(ICoffeData data);

        void Delete(ICoffeData data);

        List<ICoffeData> GetAll();

        ICoffeData? Get(int id);

        bool Any();
        
    }
}