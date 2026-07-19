using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IBusinessRepository
{
    bool AnyBusinessByOwnerId(int ownerId);
    bool AddBusiness(Business business);

    //NamNS
    List<Business> GetFeaturedHomestays(int count);
    List<Business> GetFeaturedRestaurants(int count);
    List<Business> GetHomestays(int page, int pageSize, out int totalCount, int? areaId = null, DateTime? checkIn = null, DateTime? checkOut = null, int? guests = null, int? numRooms = null, string? priceRange = null, List<int>? minRating = null, List<int>? amenityIds = null, string? sortOrder = null);
    List<Business> GetRestaurants(int page, int pageSize, out int totalCount, int? areaId = null, string? restaurantType = null, List<int>? minRating = null, string? sortOrder = null);
    Business GetHomestayDetail(int businessId);
    Business GetRestaurantDetail(int businessId);
}
