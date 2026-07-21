using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class bookingHomestayController : Controller
    {
        private readonly IHomestayService _homestayService;

        public bookingHomestayController(IHomestayService homestayService)
        {
            _homestayService = homestayService;
        }

        [Route("booking-homestay")]
        public IActionResult BookingHomestay(int roomId, int businessId)
        {
            var model = _homestayService.GetBookingInfo(roomId, businessId);
            if (model == null) return NotFound();

            return View("~/Views/Home/BookingHomestay.cshtml", model);
        }
    }
}
