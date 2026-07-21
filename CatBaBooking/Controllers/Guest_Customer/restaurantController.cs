using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class restaurantController : Controller //NamNS
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IConfiguration _configuration;

        public restaurantController(IRestaurantService restaurantService, IConfiguration configuration)
        {
            _restaurantService = restaurantService;
            _configuration = configuration;
        }

        [Route("restaurant-page")]
        public IActionResult Index(int page = 1, int? areaId = null, string? restaurantType = null, [FromQuery] List<int>? minRating = null, string? sortOrder = null)
        {
            var viewModel = _restaurantService.GetRestaurants(page, areaId, restaurantType, minRating, sortOrder);
            ViewBag.MaxStarRating = _configuration.GetValue<int>("AppSettings:MaxStarRating", 5);
            return View("~/Views/Home/Restaurant.cshtml", viewModel);
        }

        [Route("restaurant/detail/{id}")]
        public IActionResult Detail(int id)
        {
            var viewModel = _restaurantService.GetRestaurantDetail(id);

            if (viewModel == null) return NotFound();
            return View("~/Views/Home/RestaurantDetail.cshtml", viewModel);
        }

    }
}
