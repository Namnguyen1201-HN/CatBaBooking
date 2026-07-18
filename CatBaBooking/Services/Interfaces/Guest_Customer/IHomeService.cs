using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Interfaces.Guest_Customer;

public interface IHomeService //NamNS
{
    List<HomestayCardViewModel> GetFeaturedHomestays();
    List<RestaurantCardViewModel> GetFeaturedRestaurants();
}