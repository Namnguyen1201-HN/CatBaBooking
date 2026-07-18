using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Interfaces.Guest_Customer
{
    public interface IRestaurantService
    {
        RestaurantListViewModel GetRestaurants(int page); //NamNS
        RestaurantDetailViewModel GetRestaurantDetail(int businessId); //NamNS
    }
}
