using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Payment.
/// </summary>
public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(int paymentId);

    /// <summary>Lấy payment theo BookingId.</summary>
    Task<Payment?> GetByBookingIdAsync(int bookingId);

    Task<Payment> CreateAsync(Payment payment);

    /// <summary>Cập nhật trạng thái thanh toán (success / failed / pending).</summary>
    Task UpdateStatusAsync(int paymentId, string status);
}
