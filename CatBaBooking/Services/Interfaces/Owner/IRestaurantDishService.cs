using CatBaBooking.ViewModels;

namespace CatBaBooking.Service.Interface;

public interface IRestaurantDishService
{
    RestaurantManageDishesViewModel GetDishesPageData(int ownerId);

    (bool Success, string Message) AddDish(
        int ownerId, string name, decimal price, int categoryId, string? description, string? imageUrl, bool isAvailable);

    (bool Success, string Message) UpdateDish(
        int ownerId, int dishId, string name, decimal price, int categoryId, string? description, string? imageUrl, bool isAvailable);

    (bool Success, string Message) AddCategory(int ownerId, string name);
}