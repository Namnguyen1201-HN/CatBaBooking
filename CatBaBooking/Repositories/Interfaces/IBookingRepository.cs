using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Booking.
/// Một Booking có thể chứa BookedRooms (Homestay) hoặc BookedTables (Restaurant).
/// </summary>
public interface IBookingRepository
{
    // ── READ ──────────────────────────────────────────────────────────────────

    /// <summary>Lấy chi tiết booking theo ID, kèm BookedRooms + BookedTables.</summary>
    Task<Booking?> GetByIdAsync(int bookingId);

    /// <summary>Lấy lịch sử booking của một Customer.</summary>
    Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);

    /// <summary>Lấy danh sách booking thuộc về một Business (cho Owner quản lý).</summary>
    Task<IEnumerable<Booking>> GetByBusinessIdAsync(int businessId);

    // ── CREATE ────────────────────────────────────────────────────────────────

    /// <summary>Tạo booking mới.</summary>
    Task<Booking> CreateAsync(Booking booking);

    // ── UPDATE ────────────────────────────────────────────────────────────────

    /// <summary>Cập nhật trạng thái booking (confirmed / cancelled / completed).</summary>
    Task UpdateStatusAsync(int bookingId, string status);

    // ── DELETE ────────────────────────────────────────────────────────────────

    /// <summary>Hủy booking (soft-delete bằng cách đổi status).</summary>
    Task CancelAsync(int bookingId);
}
