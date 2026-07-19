using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class confirmationPaymentController : Controller
    {
        private readonly IBookingRepository _bookingRepo;

        public confirmationPaymentController(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        [Route("confirmation-payment")]
        public IActionResult ConfirmationPayment(string bookingCode)
        {
            if (string.IsNullOrEmpty(bookingCode))
            {
                return RedirectToAction("Index", "home");
            }

            var booking = _bookingRepo.GetBookingByCode(bookingCode);
            if (booking == null)
            {
                return NotFound();
            }

            var model = new ConfirmationPaymentViewModel
            {
                BookingCode = booking.BookingCode,
                BusinessName = booking.Business?.Name ?? "Nhà hàng",
                ReservationDate = booking.ReservationDate,
                ReservationTime = booking.ReservationTime,
                NumGuests = booking.NumGuests,
                TotalPrice = booking.TotalPrice
            };

            return View("~/Views/Home/ConfirmationPayment.cshtml", model);
        }
    }
}
