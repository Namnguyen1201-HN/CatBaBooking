using CatBaBooking.Helpers;
using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("user-management")]
public class userManagementController : Controller
{
    private readonly IUserManagementService _userManagementService;
    private const int PageSize = 10;

    public userManagementController(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    // GET /user-management?search=&roleId=&status=&page=
    public IActionResult Index(string? search, int? roleId, string? status, int page = 1)
    {
        var (users, totalCount) = _userManagementService.GetPagedUsers(search, roleId, status, page, PageSize);

        ViewBag.Users = users;
        ViewBag.TotalCount = totalCount;
        ViewBag.CurrentPage = PaginationHelper.NormalizePage(page, PaginationHelper.GetTotalPages(totalCount, PageSize));
        ViewBag.TotalPages = PaginationHelper.GetTotalPages(totalCount, PageSize);

        ViewBag.Search = search;
        ViewBag.RoleId = roleId;
        ViewBag.Status = status;
        ViewBag.Roles = _userManagementService.GetAssignableRoles();

        return View("~/Views/Admin/UserManagement.cshtml");
    }

    // GET /user-management/{id}
    [HttpGet("{id:int}")]
    public IActionResult Detail(int id)
    {
        var user = _userManagementService.GetUserById(id);
        if (user == null)
        {
            TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
            return Redirect("/user-management");
        }

        ViewBag.User = user;
        return View("~/Views/Admin/UserManagementDetail.cshtml");
    }

    // POST /user-management/{id}/toggle-status
    [HttpPost("{id:int}/toggle-status")]
    public IActionResult ToggleStatus(int id, string newStatus)
    {
        bool isSuccess = _userManagementService.ToggleUserStatus(id, newStatus);
        TempData[isSuccess ? "SuccessMessage" : "ErrorMessage"] =
            isSuccess ? "Cập nhật trạng thái thành công." : "Không thể cập nhật trạng thái.";
        return Redirect("/user-management");
    }
}
