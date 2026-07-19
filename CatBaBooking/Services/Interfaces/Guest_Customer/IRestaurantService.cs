using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Interfaces.Guest_Customer
{
    public interface IRestaurantService
    {
        RestaurantListViewModel GetRestaurants(int page, int? areaId = null, string? restaurantType = null, List<int>? minRating = null, string? sortOrder = null); //NamNS
        RestaurantDetailViewModel GetRestaurantDetail(int businessId); //NamNS
        CheckoutRestaurantViewModel GetCheckoutInfo(int businessId, int? userId, List<CartItemViewModel> guestCartItems = null);
        string? PlaceBooking(CheckoutRestaurantViewModel model, int? userId, List<CartItemViewModel> guestCartItems = null);
    }
}
