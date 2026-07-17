using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class DishRepository : IDishRepository
{
    private readonly CatbabookingContext _context;

    public DishRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<Dish?> GetByIdAsync(int dishId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int businessId)
    {
        // TODO: .Where(d => d.BusinessId == businessId).Include(d => d.DishCategory).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<Dish> CreateAsync(Dish dish)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Dish dish)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int dishId)
    {
        throw new NotImplementedException();
    }
}
