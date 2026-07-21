using CatBaBooking.ViewModels;

namespace CatBaBooking.Service.Interface;

public interface IRestaurantProfileService
{
    RestaurantProfileViewModel? GetProfile(int ownerId);
    (bool Success, string Message) UpdateProfile(int ownerId, RestaurantProfileViewModel model);
}