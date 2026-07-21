using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class homestayController : Controller 
    {
        private readonly IHomestayService _homestayService;
        private readonly IConfiguration _configuration;

        public homestayController(IHomestayService homestayService, IConfiguration configuration)
        {
            _homestayService = homestayService;
            _configuration = configuration;
        }

        [Route("homestay-page")]
        public IActionResult Index(int page = 1, 
                                int? areaId = null,  
                                string? priceRange = null, 
                                [FromQuery] List<int>? minRating = null, 
                                [FromQuery] List<int>? amenityIds = null, 
                                string? sortOrder = null)
        {
            var viewModel = _homestayService.GetHomestays(page, areaId, priceRange, minRating, amenityIds, sortOrder);       
            ViewBag.MaxStarRating = _configuration.GetValue<int>("AppSettings:MaxStarRating");
            return View("~/Views/Home/Homestay.cshtml", viewModel);
        }

        [Route("homestay/detail/{id}")]
        public IActionResult Detail(int id)
        {
            var viewModel = _homestayService.GetHomestayDetail(id);

            if (viewModel == null)
            {
                return NotFound();
            }
            ViewBag.MaxStarRating = _configuration.GetValue<int>("AppSettings:MaxStarRating", 5);
            return View("~/Views/Home/HomestayDetail.cshtml", viewModel);
        }

    }
}
