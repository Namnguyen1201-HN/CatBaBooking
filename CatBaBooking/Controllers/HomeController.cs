
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatBaBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult HomePage()
        {
            return View();
        }

        [Route("homestay-page")]
        public IActionResult Homestay()
        {
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
