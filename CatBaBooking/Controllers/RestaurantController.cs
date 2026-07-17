using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers;

/// <summary>
/// Xử lý trang danh sách và chi tiết Restaurant.
/// </summary>
public class RestaurantController : Controller
{
    private readonly IBusinessService _businessService;

    public RestaurantController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    /// <summary>Trang danh sách nhà hàng — GET /restaurant</summary>
    [HttpGet]
    [Route("restaurant")]
    public async Task<IActionResult> Index(int page = 1)
    {
        // TODO: var viewModel = await _businessService.GetRestaurantListAsync(page, pageSize: 9);
        // return View(viewModel);
        throw new NotImplementedException();
    }

    /// <summary>Trang chi tiết nhà hàng — GET /restaurant/detail/5</summary>
    [HttpGet]
    [Route("restaurant/detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        // TODO: var viewModel = await _businessService.GetRestaurantDetailAsync(id);
        // if (viewModel == null) return NotFound();
        // return View(viewModel);
        throw new NotImplementedException();
    }
}
