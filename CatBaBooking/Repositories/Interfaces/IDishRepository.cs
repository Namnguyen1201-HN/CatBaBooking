using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Dish (món ăn).
/// </summary>
public interface IDishRepository
{
    Task<Dish?> GetByIdAsync(int dishId);

    /// <summary>Lấy danh sách món ăn theo Restaurant.</summary>
    Task<IEnumerable<Dish>> GetByRestaurantIdAsync(int businessId);

    Task<Dish> CreateAsync(Dish dish);
    Task UpdateAsync(Dish dish);
    Task DeleteAsync(int dishId);
}
