using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CatBaBooking.Controllers.Guest_Customer;

public class homeController : Controller 
{
    private readonly IHomeService _homeService;
    private readonly IConfiguration _configuration;

    public homeController(IHomeService homeService, IConfiguration configuration)
    {
        _homeService = homeService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("")]
    [Route("home-page")]
    public IActionResult HomePage()
    {
        var featuredHomestays = _homeService.GetFeaturedHomestays();
        var featuredRestaurants = _homeService.GetFeaturedRestaurants();

        ViewBag.FeaturedHomestays = featuredHomestays;
        ViewBag.FeaturedRestaurants = featuredRestaurants;
        ViewBag.MaxStarRating = _configuration.GetValue<int>("AppSettings:MaxStarRating");

        return View();
    }
}