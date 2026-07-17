using CatBaBooking.Helpers;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers;

/// <summary>
/// Xử lý chức năng đặt chỗ: checkout, lịch sử, hủy booking.
/// Yêu cầu đăng nhập (Customer).
/// 
/// [HƯỚNG DẪN KIỂM TRA ĐĂNG NHẬP]
/// Ở đầu mỗi action cần login, thêm:
///   var user = SessionHelper.GetCurrentUser(HttpContext.Session);
///   if (user == null) return RedirectToAction("Login", "Auth");
/// </summary>
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>Trang checkout — GET /booking/checkout?businessId=5&type=Homestay</summary>
    [HttpGet]
    [Route("booking/checkout")]
    public async Task<IActionResult> Checkout(int businessId, string type)
    {
        // TODO: Kiểm tra login → Hiển thị form checkout với thông tin business
        throw new NotImplementedException();
    }

    /// <summary>Xử lý form đặt chỗ — POST /booking/checkout</summary>
    [HttpPost]
    [Route("booking/checkout")]
    public async Task<IActionResult> Checkout(BookingFormViewModel model)
    {
        // TODO:
        // 1. Kiểm tra login
        // 2. if (!ModelState.IsValid) return View(model);
        // 3. Gọi service tương ứng theo model.BookingType
        // 4. Nếu thành công → redirect sang trang thanh toán
        // 5. Nếu thất bại → return View(model) với thông báo lỗi
        throw new NotImplementedException();
    }

    /// <summary>Lịch sử đặt chỗ — GET /booking/history</summary>
    [HttpGet]
    [Route("booking/history")]
    public async Task<IActionResult> History()
    {
        // TODO: var user = GetCurrentUser → _bookingService.GetBookingHistoryAsync(user.UserId)
        throw new NotImplementedException();
    }

    /// <summary>Chi tiết booking — GET /booking/detail/5</summary>
    [HttpGet]
    [Route("booking/detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        // TODO: _bookingService.GetBookingDetailAsync(id, currentUser.UserId)
        throw new NotImplementedException();
    }

    /// <summary>Hủy booking — POST /booking/cancel/5</summary>
    [HttpPost]
    [Route("booking/cancel/{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        // TODO: _bookingService.CancelBookingAsync(id, currentUser.UserId)
        throw new NotImplementedException();
    }
}
