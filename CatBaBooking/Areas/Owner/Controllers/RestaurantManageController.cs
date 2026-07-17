using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Areas.Owner.Controllers;

/// <summary>
/// Quản lý Restaurant của Owner: listing, bàn, món ăn.
/// </summary>
[Area("Owner")]
public class RestaurantManageController : Controller
{
    private readonly IBusinessService _businessService;

    public RestaurantManageController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet]
    [Route("owner/restaurant")]
    public async Task<IActionResult> Index()
    {
        // TODO: Lấy danh sách restaurant của owner
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("owner/restaurant/create")]
    public IActionResult Create() => View();

    [HttpPost]
    [Route("owner/restaurant/create")]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePost(/* RestaurantCreateViewModel model */)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("owner/restaurant/edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("owner/restaurant/edit/{id}")]
    [ActionName("Edit")]
    public async Task<IActionResult> EditPost(int id /*, RestaurantEditViewModel model */)
    {
        throw new NotImplementedException();
    }

    /// <summary>Quản lý bàn của nhà hàng — GET /owner/restaurant/tables/5</summary>
    [HttpGet]
    [Route("owner/restaurant/tables/{businessId}")]
    public async Task<IActionResult> Tables(int businessId)
    {
        // TODO: _tableService.GetByRestaurantIdAsync(businessId)
        throw new NotImplementedException();
    }

    /// <summary>Quản lý menu món ăn — GET /owner/restaurant/menu/5</summary>
    [HttpGet]
    [Route("owner/restaurant/menu/{businessId}")]
    public async Task<IActionResult> Menu(int businessId)
    {
        // TODO: _dishService.GetByRestaurantIdAsync(businessId)
        throw new NotImplementedException();
    }
}
