using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class checkoutHomestayController : Controller
    {
        private readonly IHomestayService _homestayService;

        public checkoutHomestayController(IHomestayService homestayService)
        {
            _homestayService = homestayService;
        }

        [HttpGet]
        [Route("checkout-homestay")]
        public IActionResult CheckoutHomestay(CheckoutHomestayViewModel model)
        {
            // Note: GET method because BookingHomestay form redirects here.
            // But we actually might receive it as POST or GET depending on form method.
            // We should allow both or just GET. We will use GET from BookingHomestay.
            
            // Clear model state because GET request will not have contact info, 
            // and we don't want to show validation errors prematurely.
            ModelState.Clear();
            
            // Re-fetch room info to make sure price and names are correct
            var roomInfo = _homestayService.GetBookingInfo(model.RoomId, model.BusinessId);
            if (roomInfo == null) return NotFound();

            // Calculate total amount
            int nights = (model.CheckOut.DayNumber - model.CheckIn.DayNumber);
            if (nights <= 0) nights = 1; // Fallback

            model.RoomName = roomInfo.RoomName;
            model.HomestayName = roomInfo.HomestayName;
            model.PricePerNight = roomInfo.PricePerNight;
            model.TotalNights = nights;
            model.TotalAmount = nights * roomInfo.PricePerNight;

            return View("~/Views/Home/CheckoutHomestay.cshtml", model);
        }

        [HttpPost]
        [Route("checkout-homestay")]
        public IActionResult PlaceBooking(CheckoutHomestayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/CheckoutHomestay.cshtml", model);
            }

            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(claim, out int id))
                {
                    userId = id;
                }
            }

            // Re-calculate price to prevent tampering
            var roomInfo = _homestayService.GetBookingInfo(model.RoomId, model.BusinessId);
            if (roomInfo == null) return NotFound();
            int nights = (model.CheckOut.DayNumber - model.CheckIn.DayNumber);
            if (nights <= 0) nights = 1;
            model.TotalAmount = nights * roomInfo.PricePerNight;

            var bookingCode = _homestayService.PlaceBooking(model, userId);
            if (bookingCode != null)
            {
                return RedirectToAction("ConfirmationPayment", "confirmationPayment", new { bookingCode = bookingCode });
            }

            ModelState.AddModelError("", "Đặt phòng thất bại. Vui lòng thử lại.");
            return View("~/Views/Home/CheckoutHomestay.cshtml", model);
        }
    }
}
