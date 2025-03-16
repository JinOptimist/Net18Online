using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface ICartRepositoryReal : ICartRepository<CartData>
    {
        public int GetCartItemCount(int userId);

    }
    public class CartRepository : BaseRepository<CartData>, ICartRepositoryReal
    {
        public CartRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void AddToCart(int userId, int coffeId, int quantity)
        {
            var item = _webDbContext.CartItems.FirstOrDefault(x => x.UserId == userId && x.CoffeId == coffeId);
            if (item != null)
            {
                item.Quantity += quantity;
                _webDbContext.CartItems.Update(item);
            }
            else
            {
                _webDbContext.CartItems.Add(new CartData
                {
                    UserId = userId,
                    CoffeId = coffeId,
                    Quantity = quantity
                });
            }
            _webDbContext.SaveChanges();
        }

        public IEnumerable<CartData> GetCartItems(int userId)
        {
            return _webDbContext.CartItems
                .Where(x => x.UserId == userId)
                .Include(x => x.Coffe)
                .ToList();
        }

        public int RemoveFromCart(int userId, int coffeId, int quantity)
        {
            var item = _webDbContext.CartItems.FirstOrDefault(x => x.UserId == userId && x.CoffeId == coffeId);

            if (item != null)
            {
                if (item.Quantity > quantity)
                {
                    item.Quantity -= quantity;
                }
                else
                {
                    _webDbContext.CartItems.Remove(item);
                    _webDbContext.SaveChanges();
                    return 0;
                }

                _webDbContext.SaveChanges();
                return item.Quantity;
            }

            return -1;
        }


        public int GetCartItemCount(int userId)
        {
            return _webDbContext.CartItems
                .Where(c => c.UserId == userId)
                .Sum(c => c.Quantity);
        }
    }
}
