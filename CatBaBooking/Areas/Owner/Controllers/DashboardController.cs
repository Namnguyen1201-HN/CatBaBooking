using CatBaBooking.Helpers;
using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Owner.Controllers;

/// <summary>
/// Dashboard chính của Owner — tổng quan doanh thu, booking.
/// 
/// [HƯỚNG DẪN AREA]
/// - Area "Owner" tạo namespace riêng và route riêng (/owner/...)
/// - Attribute [Area("Owner")] khai báo controller này thuộc area nào
/// - Cần thêm route Area trong Program.cs (đã có sẵn template)
/// 
/// [PHÂN QUYỀN]
/// Đầu mỗi action, kiểm tra:
///   if (!SessionHelper.IsInRole(session, "HomestayOwner") && 
///       !SessionHelper.IsInRole(session, "RestaurantOwner"))
///       return RedirectToAction("Login", "Auth", new { area = "" });
/// </summary>
[Area("Owner")]
public class DashboardController : Controller
{
    private readonly IBusinessService _businessService;
    private readonly IBookingService _bookingService;

    public DashboardController(IBusinessService businessService, IBookingService bookingService)
    {
        _businessService = businessService;
        _bookingService = bookingService;
    }

    /// <summary>Trang chủ Owner Dashboard — GET /owner/dashboard</summary>
    [HttpGet]
    [Route("owner/dashboard")]
    public async Task<IActionResult> Index()
    {
        // TODO:
        // 1. Kiểm tra quyền Owner
        // 2. Lấy danh sách business của owner: _businessService.GetOwnerBusinessesAsync(ownerId)
        // 3. Lấy thống kê tổng quan (số booking, doanh thu)
        // 4. return View(dashboardViewModel)
        throw new NotImplementedException();
    }
}
