using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Admin.Controllers;

/// <summary>
/// Admin quản lý user: xem danh sách, khóa/mở khóa tài khoản.
/// </summary>
[Area("Admin")]
public class UserManageController : Controller
{
    private readonly IUserService _userService;

    public UserManageController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Danh sách tất cả user — GET /admin/users</summary>
    [HttpGet]
    [Route("admin/users")]
    public async Task<IActionResult> Index()
    {
        // TODO: var users = await _userService.GetAllUsersAsync();
        // return View(users);
        throw new NotImplementedException();
    }

    /// <summary>Khóa hoặc mở khóa tài khoản — POST /admin/users/toggle-status/5</summary>
    [HttpPost]
    [Route("admin/users/toggle-status/{userId}")]
    public async Task<IActionResult> ToggleStatus(int userId, string newStatus)
    {
        // TODO: await _userService.UpdateUserStatusAsync(userId, newStatus);
        // return RedirectToAction("Index");
        throw new NotImplementedException();
    }
}
