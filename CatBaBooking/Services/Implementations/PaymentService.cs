using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Payment;

namespace CatBaBooking.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBookingRepository _bookingRepository;

    public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository)
    {
        _paymentRepository = paymentRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<string> CreatePaymentUrlAsync(int bookingId, decimal amount, string returnUrl)
    {
        // TODO (Lựa chọn 1 — Mock Payment):
        //   Tạo Payment record với status = "pending"
        //   Trả về URL tới trang thanh toán giả lập của app
        //
        // TODO (Lựa chọn 2 — VNPay):
        //   Dùng VNPay SDK để tạo URL redirect
        //   Tham khảo: https://sandbox.vnpayment.vn/apis/
        throw new NotImplementedException();
    }

    public async Task<PaymentResultViewModel> HandlePaymentCallbackAsync(IQueryCollection queryParams)
    {
        // TODO (Mock): Đổi status Payment → "success", Booking → "confirmed"
        // TODO (VNPay): Xác thực chữ ký HMAC → parse kết quả → update status
        throw new NotImplementedException();
    }

    public async Task<PaymentResultViewModel?> GetPaymentInfoAsync(int bookingId)
    {
        // TODO: _paymentRepository.GetByBookingIdAsync(bookingId) → map → ViewModel
        throw new NotImplementedException();
    }
}
