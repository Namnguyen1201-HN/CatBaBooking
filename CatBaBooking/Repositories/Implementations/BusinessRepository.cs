using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repositories.Implementations;

/// <summary>
/// Triển khai IBusinessRepository.
/// Business bao gồm cả Homestay và Restaurant — phân biệt qua BusinessType.
/// </summary>
public class BusinessRepository : IBusinessRepository
{
    private readonly CatbabookingContext _context;

    public BusinessRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Business>> GetAllActiveAsync()
    {
        // TODO: Lấy tất cả business có Status = "approved"
        // Gợi ý: .Where(b => b.Status == "approved").ToListAsync()
        throw new NotImplementedException();
    }

    public Task<Business?> GetByIdAsync(int businessId)
    {
        // TODO: .Include(b => b.Amenities).Include(b => b.Reviews) ... .FindAsync(businessId)
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetByOwnerIdAsync(int ownerId)
    {
        // TODO: .Where(b => b.OwnerId == ownerId).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetPendingAsync()
    {
        // TODO: .Where(b => b.Status == "pending").ToListAsync()
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Business>> GetHomestaysAsync(int page, int pageSize)
    {
        return await _context.Businesses
            .Where(b => b.Type == "homestay" && b.Status == "active")
            .OrderByDescending(b => b.AvgRating)
            .Include(b => b.Area)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<IEnumerable<Business>> GetRestaurantsAsync(int page, int pageSize)
    {
        // TODO: .Where(b => b.BusinessType == "Restaurant")
        //        .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
        throw new NotImplementedException();
    }

    public async Task<int> CountHomestaysAsync()
    {
        return await _context.Businesses
                     .CountAsync(b => b.Type == "homestay" && b.Status == "active");
    }

    public Task<int> CountRestaurantsAsync()
    {
        // TODO: .CountAsync(b => b.BusinessType == "Restaurant" && b.Status == "approved")
        throw new NotImplementedException();
    }

    public Task<Business> CreateAsync(Business business)
    {
        // TODO: _context.Businesses.Add(business); SaveChanges; return business;
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Business business)
    {
        // TODO: _context.Businesses.Update(business); SaveChanges;
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int businessId, string status)
    {
        // TODO: Tìm business, đổi Status, SaveChanges
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int businessId)
    {
        // TODO: Tìm business, Remove, SaveChanges
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Business>> GetFeaturedHomestaysAsync(int top) //NamNS
    {
        return await _context.Businesses
                    .Where(b => b.Type == "homestay" && b.Status == "active")
                    .OrderByDescending(b => b.AvgRating)
                    .ThenByDescending(b => b.ReviewCount)
                    .Take(top)
                    .Include(b => b.Area)
                    .ToListAsync();
    }

    public async Task<IEnumerable<Business>> GetFeaturedRestaurantAsync(int top) //NamNS
    {
        return await _context.Businesses
                    .Where(b => b.Type == "restaurant" && b.Status == "active")
                    .OrderByDescending(b => b.AvgRating)
                    .ThenByDescending(b => b.ReviewCount)
                    .Take(top)
                    .Include(b => b.Area)
                    .ToListAsync();
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
            .FirstOrDefault(b => b.BusinessId == businessId && b.Type == "homestay");
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
