using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Business.
/// Business là entity cha chứa cả Homestay lẫn Restaurant.
/// </summary>
public interface IBusinessRepository
{
    bool AnyBusinessByOwnerId(int ownerId);
    bool AddBusiness(Business business);

    //NamNS
    List<Business> GetFeaturedHomestays(int count);
    List<Business> GetFeaturedRestaurants(int count);
    List<Business> GetHomestays(int page, int pageSize, out int totalCount);
    List<Business> GetRestaurants(int page, int pageSize, out int totalCount);
    Business GetHomestayDetail(int businessId);
    Business GetRestaurantDetail(int businessId);

}
