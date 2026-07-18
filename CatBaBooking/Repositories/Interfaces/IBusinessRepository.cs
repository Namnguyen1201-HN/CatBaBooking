using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

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
