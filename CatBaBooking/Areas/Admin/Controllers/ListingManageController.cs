using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Admin.Controllers;

/// <summary>
/// Admin quản lý listings (homestay + restaurant):
/// xem danh sách chờ duyệt, phê duyệt, từ chối, xóa.
/// </summary>
[Area("Admin")]
public class ListingManageController : Controller
{
    private readonly IBusinessService _businessService;

    public ListingManageController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    /// <summary>Danh sách tất cả listing — GET /admin/listings</summary>
    [HttpGet]
    [Route("admin/listings")]
    public async Task<IActionResult> Index()
    {
        throw new NotImplementedException();
    }

    /// <summary>Listing đang chờ duyệt — GET /admin/listings/pending</summary>
    [HttpGet]
    [Route("admin/listings/pending")]
    public async Task<IActionResult> Pending()
    {
        // TODO: var pending = await _businessService.GetPendingListingsAsync();
        // return View(pending);
        throw new NotImplementedException();
    }

    /// <summary>Phê duyệt hoặc từ chối listing — POST /admin/listings/update-status/5</summary>
    [HttpPost]
    [Route("admin/listings/update-status/{businessId}")]
    public async Task<IActionResult> UpdateStatus(int businessId, string status)
    {
        // TODO: await _businessService.UpdateListingStatusAsync(businessId, status);
        // return RedirectToAction("Pending");
        throw new NotImplementedException();
    }

    /// <summary>Xóa listing — POST /admin/listings/delete/5</summary>
    [HttpPost]
    [Route("admin/listings/delete/{businessId}")]
    public async Task<IActionResult> Delete(int businessId)
    {
        // TODO: await _businessService.DeleteBusinessAsync(businessId, adminId);
        throw new NotImplementedException();
    }
}
