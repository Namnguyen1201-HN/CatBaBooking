using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho RestaurantTable (bàn nhà hàng).
/// </summary>
public interface IRestaurantTableRepository
{
    Task<RestaurantTable?> GetByIdAsync(int tableId);

    /// <summary>Lấy danh sách bàn theo Restaurant.</summary>
    Task<IEnumerable<RestaurantTable>> GetByRestaurantIdAsync(int businessId);

    /// <summary>Lấy danh sách bàn còn trống vào ngày + giờ cụ thể.</summary>
    Task<IEnumerable<RestaurantTable>> GetAvailableTablesAsync(int businessId, DateOnly date, TimeOnly time);

    Task<RestaurantTable> CreateAsync(RestaurantTable table);
    Task UpdateAsync(RestaurantTable table);
    Task DeleteAsync(int tableId);
}
