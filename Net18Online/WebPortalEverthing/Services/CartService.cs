using Everything.Data.Repositories;

namespace WebPortalEverthing.Services
{
    public class CartService
    {
        private ICartRepositoryReal _cartRepository;

        public CartService(ICartRepositoryReal cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public int GetCartItemCount(int userId)
        {
            return _cartRepository.GetCartItemCount(userId);
        }
    }
}
