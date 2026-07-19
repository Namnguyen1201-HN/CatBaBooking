using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class BusinessRepository : IBusinessRepository
{
    private readonly CatbabookingContext _context;

    public BusinessRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public bool AnyBusinessByOwnerId(int ownerId)
    {
        return _context.Businesses.Any(x => x.OwnerId == ownerId);
    }

    public bool AddBusiness(Business business)
    {
        _context.Businesses.Add(business);
        _context.SaveChanges();
        return true;
    }

    public List<Business> GetFeaturedHomestays(int count) //NamNS
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Rooms)
            .Where(b => b.Type == "Homestay")
            .OrderByDescending(b => b.AvgRating)
            .Take(count)
            .ToList();
    }

    public List<Business> GetFeaturedRestaurants(int count) //NamNS
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Where(b => b.Type == "Restaurant")
            .OrderByDescending(b => b.AvgRating)
            .Take(count)
            .ToList(); 
    }

    public List<Business> GetHomestays(int page, int pageSize, out int totalCount) //NamNS
    {
        var query = _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Rooms)
            .Where(b => b.Type == "Homestay" && b.Status == "active"); 

            totalCount = query.Count();
  
        return query
            .OrderByDescending(b => b.AvgRating)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public List<Business> GetRestaurants(int page, int pageSize, out int totalCount) //NamNS
    {
        var query = _context.Businesses
            .Include(b => b.Area)
            .Where(b => b.Type == "Restaurant" && b.Status == "active");

        totalCount = query.Count();
        return query
            .OrderByDescending(b => b.AvgRating)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Business GetHomestayDetail(int businessId) //NamNS
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Rooms)
            .Include(b => b.Amenities)
            .Include(b => b.Reviews)
            .ThenInclude(r => r.User)
            .Include(b => b.Owner)
            .FirstOrDefault(b => b.BusinessId == businessId && b.Type.ToLower() == "homestay");
    }

    public Business GetRestaurantDetail(int businessId) //NamNS
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.RestaurantTables) 
            .Include(b => b.DishCategories) 
            .Include(b => b.Dishes)         
            .Include(b => b.Reviews)
            .ThenInclude(r => r.User)
            .FirstOrDefault(b => b.BusinessId == businessId && b.Type == "Restaurant");
    }

}
