using CatBaBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Owner
{
    public class RestaurantBookingsController : Controller
    {
        private readonly IRestaurantBookingService _bookingService;

        public RestaurantBookingsController(IRestaurantBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        
        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : null;
        }

        [HttpGet]
        public IActionResult RestaurantBookings(
            DateOnly? reservationDate,
            TimeOnly? reservationTime,
            int? numGuests,
            string? status,
            string? searchCode,
            int page = 1)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var viewModel = _bookingService.GetBookings(
                userId.Value, reservationDate, reservationTime, numGuests, status, searchCode, page);

            return View("~/Views/Owner/RestaurantBookings.cshtml", viewModel);
        }
    }
}