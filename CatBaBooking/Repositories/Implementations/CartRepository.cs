using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CatBaBooking.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly CatbabookingContext _context;

        public CartRepository(CatbabookingContext context)
        {
            _context = context;
        }

        public List<TempCart> GetCartItems(int businessId, int userId)
        {
            return _context.TempCarts
                .Include(c => c.Dish)
                .Where(c => c.BusinessId == businessId && c.UserId == userId)
                .ToList();
        }

        public void SaveCartItems(int businessId, int userId, List<TempCart> items)
        {
            ClearUserCart(businessId, userId);
            
            if (items != null && items.Any())
            {
                _context.TempCarts.AddRange(items);
                _context.SaveChanges();
            }
        }

        public void ClearUserCart(int businessId, int userId)
        {
            var existingItems = _context.TempCarts
                .Where(c => c.BusinessId == businessId && c.UserId == userId)
                .ToList();

            if (existingItems.Any())
            {
                _context.TempCarts.RemoveRange(existingItems);
                _context.SaveChanges();
            }
        }
    }
}
