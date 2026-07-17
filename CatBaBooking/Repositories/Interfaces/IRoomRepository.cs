using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Room (phòng của Homestay).
/// </summary>
public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(int roomId);

    /// <summary>Lấy danh sách phòng theo Homestay.</summary>
    Task<IEnumerable<Room>> GetByHomestayIdAsync(int businessId);

    /// <summary>Lấy danh sách phòng còn trống trong khoảng ngày check-in/out.</summary>
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(int businessId, DateOnly checkIn, DateOnly checkOut);

    Task<Room> CreateAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(int roomId);
}
