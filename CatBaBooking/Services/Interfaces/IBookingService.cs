using CatBaBooking.Models;
using CatBaBooking.ViewModels.Booking;

namespace CatBaBooking.Services.Interfaces;

/// <summary>
/// Tầng Service xử lý business logic cho Booking.
/// Đây là Service phức tạp nhất — phối hợp nhiều repo (Booking, Room, Table, Payment).
/// </summary>
public interface IBookingService
{
    /// <summary>
    /// Lấy chi tiết booking kèm thông tin phòng/bàn đã đặt.
    /// </summary>
    Task<BookingDetailViewModel?> GetBookingDetailAsync(int bookingId, int requesterId);

    /// <summary>
    /// Lấy lịch sử đặt chỗ của một Customer.
    /// </summary>
    Task<IEnumerable<BookingHistoryViewModel>> GetBookingHistoryAsync(int userId);

    /// <summary>
    /// Tạo booking Homestay.
    /// [Service sẽ: kiểm tra room còn trống → tạo Booking + BookedRooms → trả về bookingId]
    /// </summary>
    Task<(bool Success, int BookingId, string ErrorMessage)> CreateHomestayBookingAsync(BookingFormViewModel model, int userId);

    /// <summary>
    /// Tạo booking Restaurant (đặt bàn + chọn món).
    /// [Service sẽ: kiểm tra table còn trống → tạo Booking + BookedTables + BookingDishes]
    /// </summary>
    Task<(bool Success, int BookingId, string ErrorMessage)> CreateRestaurantBookingAsync(BookingFormViewModel model, int userId);

    /// <summary>
    /// Hủy booking (Customer hủy đặt chỗ của mình).
    /// [Service sẽ: kiểm tra booking thuộc về userId → đổi status → cập nhật availability]
    /// </summary>
    Task<bool> CancelBookingAsync(int bookingId, int userId);

    /// <summary>
    /// Owner xác nhận hoặc từ chối booking.
    /// </summary>
    Task<bool> UpdateBookingStatusAsync(int bookingId, string status, int ownerId);

}
