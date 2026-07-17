using CatBaBooking.ViewModels.Payment;

namespace CatBaBooking.Services.Interfaces;

/// <summary>
/// Tầng Service xử lý business logic thanh toán.
/// Có thể tích hợp VNPay hoặc dùng mock thanh toán.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Tạo URL thanh toán (chuyển hướng sang cổng thanh toán VNPay).
    /// [Nếu dùng mock: chỉ cần tạo Payment record với status pending]
    /// </summary>
    Task<string> CreatePaymentUrlAsync(int bookingId, decimal amount, string returnUrl);

    /// <summary>
    /// Xử lý callback từ cổng thanh toán sau khi user thanh toán xong.
    /// [Service sẽ: xác thực chữ ký → cập nhật Payment status → cập nhật Booking status]
    /// </summary>
    Task<PaymentResultViewModel> HandlePaymentCallbackAsync(IQueryCollection queryParams);

    /// <summary>Lấy thông tin thanh toán theo BookingId.</summary>
    Task<PaymentResultViewModel?> GetPaymentInfoAsync(int bookingId);
}
