using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class RestaurantTableRepository : IRestaurantTableRepository
{
    private readonly CatbabookingContext _context;

    public RestaurantTableRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public List<RestaurantTable> GetByBusinessId(int businessId)
    {
        return _context.RestaurantTables
            .Where(t => t.BusinessId == businessId)
            .OrderBy(t => t.TableId)
            .ToList();
    }

    public RestaurantTable? GetById(int tableId)
    {
        return _context.RestaurantTables.FirstOrDefault(t => t.TableId == tableId);
    }

    public void Add(RestaurantTable table)
    {
        _context.RestaurantTables.Add(table);
        _context.SaveChanges();
    }

    public void Update(RestaurantTable table)
    {
        _context.RestaurantTables.Update(table);
        _context.SaveChanges();
    }

    public void Delete(RestaurantTable table)
    {
        _context.RestaurantTables.Remove(table);
        _context.SaveChanges();
    }

    public bool HasBookings(int tableId)
    {
        return _context.BookedTables.Any(bt => bt.TableId == tableId);
    }
}