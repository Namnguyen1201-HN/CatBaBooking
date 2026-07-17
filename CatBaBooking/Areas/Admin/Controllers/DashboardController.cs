using CatBaBooking.Helpers;
using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Admin.Controllers;

/// <summary>
/// Dashboard Admin — quản lý toàn hệ thống.
/// CHỈ Admin mới được vào.
/// 
/// [PHÂN QUYỀN]
/// Thêm vào đầu action: if (!SessionHelper.IsInRole(session, "Admin")) return Forbid();
/// </summary>
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IUserService _userService;
    private readonly IBusinessService _businessService;

    public DashboardController(IUserService userService, IBusinessService businessService)
    {
        _userService = userService;
        _businessService = businessService;
    }

    /// <summary>Trang tổng quan Admin — GET /admin/dashboard</summary>
    [HttpGet]
    [Route("admin/dashboard")]
    public async Task<IActionResult> Index()
    {
        // TODO: Thống kê: tổng user, tổng listing, listing chờ duyệt...
        throw new NotImplementedException();
    }
}
