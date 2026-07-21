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

    public List<Business> GetFeaturedHomestays(int count) 
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Rooms)
            .Where(b => b.Type == "Homestay")
            .OrderByDescending(b => b.AvgRating)
            .Take(count)
            .ToList();
    }

    public List<Business> GetFeaturedRestaurants(int count) 
    {
        return _context.Businesses
            .Include(b => b.Area)
            .Where(b => b.Type == "Restaurant")
            .OrderByDescending(b => b.AvgRating)
            .Take(count)
            .ToList(); 
    }

    public List<Business> GetHomestays(int page, int pageSize, out int totalCount, 
                                        int? areaId = null,                                                                         
                                        string? priceRange = null, 
                                        List<int>? minRating = null, 
                                        List<int>? amenityIds = null, string? sortOrder = null) 
    {
        var query = _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Rooms)
            .Include(b => b.Amenities)
            .Where(b => b.Type == "Homestay" && b.Status == "active"); 

        //Fillter động
        if (areaId.HasValue && areaId.Value > 0)
        {
            query = query.Where(b => b.AreaId == areaId.Value);
        }

        if (!string.IsNullOrEmpty(priceRange))
        {
            var parts = priceRange.Split('-');
            if (parts.Length == 2 && decimal.TryParse(parts[0], out decimal minPrice) && decimal.TryParse(parts[1], out decimal maxPrice))
            {
                if (maxPrice == 0) // e.g. "1000000-0" for 1,000,000+
                {
                    query = query.Where(b => (b.Rooms.Any() ? b.Rooms.Min(r => r.PricePerNight) : b.PricePerNight) >= minPrice);
                }
                else
                {
                    query = query.Where(b => (b.Rooms.Any() ? b.Rooms.Min(r => r.PricePerNight) : b.PricePerNight) >= minPrice && 
                                             (b.Rooms.Any() ? b.Rooms.Min(r => r.PricePerNight) : b.PricePerNight) <= maxPrice);
                }
            }
        }

        if (minRating != null && minRating.Any())
        {
            var minR = minRating.Min();
            query = query.Where(b => b.AvgRating >= minR);
        }

        if (amenityIds != null && amenityIds.Any())
        {
            foreach (var aid in amenityIds)
            {
                query = query.Where(b => b.Amenities.Any(a => a.AmenityId == aid));
            }
        }

        totalCount = query.Count();
  
        switch (sortOrder)
        {
            case "Giá thấp đến cao":
                query = query.OrderBy(b => b.Rooms.Any() ? b.Rooms.Min(r => r.PricePerNight) : b.PricePerNight);
                break;
            case "Giá cao đến thấp":
                query = query.OrderByDescending(b => b.Rooms.Any() ? b.Rooms.Min(r => r.PricePerNight) : b.PricePerNight);
                break;
            case "Đánh giá cao nhất":
                query = query.OrderByDescending(b => b.AvgRating);
                break;
            case "Phù hợp nhất":
            default:
                query = query.OrderByDescending(b => b.AvgRating).ThenBy(b => b.BusinessId);
                break;
        }

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList(); 
    }

    public List<Business> GetRestaurants(int page, int pageSize, out int totalCount, 
                                            int? areaId = null, 
                                            string? restaurantType = null, 
                                            List<int>? minRating = null, 
                                            string? sortOrder = null) 
    {
        var query = _context.Businesses
            .Include(b => b.Area)
            .Include(b => b.Types)
            .Where(b => b.Type == "Restaurant" && b.Status == "active");

        if (areaId.HasValue && areaId.Value > 0)
        {
            query = query.Where(b => b.AreaId == areaId.Value);
        }

        if (!string.IsNullOrEmpty(restaurantType))
        {
            // Assuming restaurantType from UI is an ID like "1", "2", "3"
            if (int.TryParse(restaurantType, out int typeId))
            {
                query = query.Where(b => b.Types.Any(t => t.TypeId == typeId));
            }
        }

        if (minRating != null && minRating.Any())
        {
            var minR = minRating.Min();
            query = query.Where(b => b.AvgRating >= minR);
        }

        totalCount = query.Count();

        if (sortOrder == "Đánh giá cao nhất")
        {
            query = query.OrderByDescending(b => b.AvgRating);
        }
        else // Phù hợp nhất -> theo số lượng review
        {
            query = query.OrderByDescending(b => b.ReviewCount).ThenByDescending(b => b.AvgRating);
        }

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Business GetHomestayDetail(int businessId) 
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

    public Business GetRestaurantDetail(int businessId) 
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

    public Business? GetByOwnerId(int ownerId)
    {
        return _context.Businesses.FirstOrDefault(x => x.OwnerId == ownerId);
    }

    public void Update(Business business)
    {
        _context.Businesses.Update(business);
        _context.SaveChanges();
    }
}