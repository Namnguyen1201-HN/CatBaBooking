using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Guest_Customer;

public class homeController : Controller 
{
    private readonly IHomeService _homeService;

    public homeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet]
    [Route("")]
    [Route("home-page")]
    public IActionResult Index()
    {
        var featuredHomestays = _homeService.GetFeaturedHomestays();
        var featuredRestaurants = _homeService.GetFeaturedRestaurants();

        ViewBag.FeaturedHomestays = featuredHomestays;
        ViewBag.FeaturedRestaurants = featuredRestaurants;

        return View("~/Views/Home/HomePage.cshtml");
    }
}