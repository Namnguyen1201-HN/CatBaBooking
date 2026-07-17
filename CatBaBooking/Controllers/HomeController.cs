
using CatBaBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatBaBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusinessService _businessService;

        public HomeController(ILogger<HomeController> logger, IBusinessService businessService)
        {
            _logger = logger;
            _businessService = businessService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("")]
        [Route("home-page")]
        public async Task<IActionResult> HomePage()
        {
            var featuredHomestays = await _businessService.GetFeaturedHomestaysAsync(top: 3);
            var featuredRestaurant = await _businessService.GetFeaturedRestaurantAsync(top: 3);

            ViewBag.FeaturedHomestays = featuredHomestays;
            ViewBag.FeaturedRestaurants = featuredRestaurant;
            return View();
        }


        [Route("homestay-detail")]
        public IActionResult HomestayDetail()
        {
            return View();
        }

        [Route("restaurant-page")]
        public IActionResult Restaurant()
        {
            return View();
        }

        [Route("restaurant-detail")]
        public IActionResult RestaurantDetail()
        {
            return View();
        }

        [Route("checkout-restaurant")]
        public IActionResult CheckoutRestaurant()
        {
            return View();
        }

        [Route("checkout-homestay")]
        public IActionResult CheckoutHomestay()
        {
            return View();
        }

        [Route("booking-homestay")]
        public IActionResult BookingHomestay()
        {
            return View();
        }

        [Route("confirmation-payment")]
        public IActionResult ConfirmationPayment()
        {
            return View();
        }

        
    }
}
