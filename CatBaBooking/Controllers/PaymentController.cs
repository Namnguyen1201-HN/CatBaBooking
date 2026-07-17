using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers;

/// <summary>
/// Xử lý flow thanh toán.
/// Sau khi tạo booking thành công → redirect sang đây để thanh toán.
/// </summary>
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>Trang thanh toán — GET /payment/checkout/5 (bookingId)</summary>
    [HttpGet]
    [Route("payment/checkout/{bookingId}")]
    public async Task<IActionResult> Checkout(int bookingId)
    {
        // TODO: Tạo URL thanh toán → redirect sang VNPay HOẶC hiển thị form mock
        // var url = await _paymentService.CreatePaymentUrlAsync(bookingId, amount, returnUrl);
        // return Redirect(url);
        throw new NotImplementedException();
    }

    /// <summary>
    /// Callback từ VNPay sau khi thanh toán — GET /payment/callback
    /// VNPay sẽ redirect user về URL này với các query params chứa kết quả.
    /// </summary>
    [HttpGet]
    [Route("payment/callback")]
    public async Task<IActionResult> Callback()
    {
        // TODO: var result = await _paymentService.HandlePaymentCallbackAsync(Request.Query);
        // return View("PaymentResult", result);
        throw new NotImplementedException();
    }

    /// <summary>Trang kết quả thanh toán — GET /payment/result/5</summary>
    [HttpGet]
    [Route("payment/result/{bookingId}")]
    public async Task<IActionResult> Result(int bookingId)
    {
        // TODO: _paymentService.GetPaymentInfoAsync(bookingId)
        throw new NotImplementedException();
    }
}
