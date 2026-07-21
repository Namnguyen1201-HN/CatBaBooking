using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IRestaurantTableRepository
{
    List<RestaurantTable> GetByBusinessId(int businessId);
    RestaurantTable? GetById(int tableId);
    void Add(RestaurantTable table);
    void Update(RestaurantTable table);
    void Delete(RestaurantTable table);

    // Kiểm tra bàn đã từng có lịch đặt hay chưa (để chặn xoá, tránh lỗi khoá ngoại)
    bool HasBookings(int tableId);
}