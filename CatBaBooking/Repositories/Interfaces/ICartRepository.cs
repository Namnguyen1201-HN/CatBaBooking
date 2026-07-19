using CatBaBooking.Models;
using System.Collections.Generic;

namespace CatBaBooking.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<TempCart> GetCartItems(int businessId, int userId);
        void SaveCartItems(int businessId, int userId, List<TempCart> items);
        void ClearUserCart(int businessId, int userId);
    }
}
