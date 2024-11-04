using Everything.Data.Interface.Models;
using System.Text;

namespace Everything.Data.Interface.Repositories
{
    public interface ICoffeShopRepository<T> : IBaseRepository<T>
        where T : ICoffeData
    {
        void UpdateCoffeName(int id, string name);
        
        void UpdateImage(int id, string url);
        
        void UpdateCost(int id, decimal cost);

        void UpdateBrand(int id, string brand);
    }
}