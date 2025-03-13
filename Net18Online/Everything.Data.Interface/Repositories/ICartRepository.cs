using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICartRepository<T> : IBaseRepository<T>
        where T : ICartData
    {
        void AddToCart(int userId, int coffeId, int quantity);
        void RemoveFromCart(int userId, int coffeId, int quantity);
        IEnumerable<T> GetCartItems(int userId);
    }
}
