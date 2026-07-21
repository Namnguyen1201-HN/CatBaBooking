using CatBaBooking.Models;
using System.Collections.Generic;

namespace CatBaBooking.Repository.Interface;

public interface IBusinessRepository
{
    bool AnyBusinessByOwnerId(int ownerId);
    bool AddBusiness(Business business);

    // Lấy business (nhà hàng/homestay) đầu tiên thuộc về 1 owner - dùng cho Owner Dashboard
    Business? GetByOwnerId(int ownerId);

    // Cập nhật thông tin business (dùng cho trang Thông tin Nhà hàng)
    void Update(Business business);

    List<Business> GetFeaturedHomestays(int count);
    List<Business> GetFeaturedRestaurants(int count);

    List<Business> GetHomestays(int page, int pageSize, out int totalCount,
                                int? areaId = null,
                                string? priceRange = null,
                                List<int>? minRating = null,
                                List<int>? amenityIds = null,
                                string? sortOrder = null);

    List<Business> GetRestaurants(int page, int pageSize, out int totalCount,
                                int? areaId = null,
                                string? restaurantType = null,
                                List<int>? minRating = null,
                                string? sortOrder = null);

    Business GetHomestayDetail(int businessId);
    Business GetRestaurantDetail(int businessId);

    List<Area> GetAllAreas();
    List<Amenity> GetAllAmenities();
    List<RestaurantType> GetAllRestaurantTypes();
}