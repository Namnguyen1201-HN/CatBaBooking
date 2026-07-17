using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Owner.Controllers;

/// <summary>
/// Quản lý Homestay của Owner: xem, thêm, sửa, xóa listing.
/// Chỉ Homestay Owner mới truy cập được.
/// </summary>
[Area("Owner")]
public class HomestayManageController : Controller
{
    private readonly IBusinessService _businessService;

    public HomestayManageController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    /// <summary>Danh sách homestay của owner — GET /owner/homestay</summary>
    [HttpGet]
    [Route("owner/homestay")]
    public async Task<IActionResult> Index()
    {
        // TODO: _businessService.GetOwnerBusinessesAsync(ownerId) → lọc type Homestay
        throw new NotImplementedException();
    }

    /// <summary>Form thêm homestay mới — GET /owner/homestay/create</summary>
    [HttpGet]
    [Route("owner/homestay/create")]
    public IActionResult Create() => View();

    /// <summary>Xử lý thêm homestay — POST /owner/homestay/create</summary>
    [HttpPost]
    [Route("owner/homestay/create")]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePost(/* HomestayCreateViewModel model */)
    {
        // TODO: validate → _businessService.CreateBusinessAsync(business)
        throw new NotImplementedException();
    }

    /// <summary>Form sửa homestay — GET /owner/homestay/edit/5</summary>
    [HttpGet]
    [Route("owner/homestay/edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        // TODO: Lấy thông tin homestay → hiển thị form
        throw new NotImplementedException();
    }

    /// <summary>Xử lý sửa homestay — POST /owner/homestay/edit/5</summary>
    [HttpPost]
    [Route("owner/homestay/edit/{id}")]
    [ActionName("Edit")]
    public async Task<IActionResult> EditPost(int id /*, HomestayEditViewModel model */)
    {
        // TODO: _businessService.UpdateBusinessAsync(business, ownerId)
        throw new NotImplementedException();
    }

    /// <summary>Xóa homestay — POST /owner/homestay/delete/5</summary>
    [HttpPost]
    [Route("owner/homestay/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // TODO: _businessService.DeleteBusinessAsync(id, ownerId)
        throw new NotImplementedException();
    }

    /// <summary>Xem danh sách booking của homestay — GET /owner/homestay/bookings/5</summary>
    [HttpGet]
    [Route("owner/homestay/bookings/{businessId}")]
    public async Task<IActionResult> Bookings(int businessId)
    {
        // TODO: _bookingService.GetBookingsByBusinessIdAsync(businessId, ownerId)
        throw new NotImplementedException();
    }
}
