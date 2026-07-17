using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class RestaurantTableRepository : IRestaurantTableRepository
{
    private readonly CatbabookingContext _context;

    public RestaurantTableRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<RestaurantTable?> GetByIdAsync(int tableId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RestaurantTable>> GetByRestaurantIdAsync(int businessId)
    {
        // TODO: .Where(t => t.BusinessId == businessId).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RestaurantTable>> GetAvailableTablesAsync(int businessId, DateOnly date, TimeOnly time)
    {
        // TODO: Kiểm tra TableAvailability xem bàn nào còn trống vào ngày + giờ đó
        throw new NotImplementedException();
    }

    public Task<RestaurantTable> CreateAsync(RestaurantTable table)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RestaurantTable table)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int tableId)
    {
        throw new NotImplementedException();
    }
}
