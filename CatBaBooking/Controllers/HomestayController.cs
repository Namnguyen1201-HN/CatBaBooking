using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers;

/// <summary>
/// Xử lý trang danh sách và chi tiết Homestay.
/// Dành cho Guest và Customer (không cần đăng nhập để xem).
/// </summary>
public class HomestayController : Controller
{
    private readonly IBusinessService _businessService;

    public HomestayController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    /// <summary>Trang danh sách homestay — GET /homestay</summary>
    [HttpGet]
    [Route("homestay")]
    public async Task<IActionResult> Index(int page = 1)
    {
        const int pageSize = 6;
        var viewModel = await _businessService.GetHomestayListAsync(page, pageSize);
        return View("~/Views/Home/Homestay.cshtml", viewModel);
    }

    /// <summary>Trang chi tiết homestay — GET /homestay/detail/5</summary>
    [HttpGet]
    [Route("homestay/detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        // TODO:
        // var viewModel = await _businessService.GetHomestayDetailAsync(id);
        // if (viewModel == null) return NotFound();
        // return View(viewModel);
        throw new NotImplementedException();
    }
}
