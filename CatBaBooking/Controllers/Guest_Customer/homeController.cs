using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Guest_Customer;

public class homeController : Controller //NamNS
{
    private readonly IHomeService _homeService;
    private readonly IBookingRepository _bookingRepo;
    private readonly IHomestayService _homestayService;

    //Inject Service
    public homeController(IHomeService homeService, IBookingRepository bookingRepo, IHomestayService homestayService)
    {
        _homeService = homeService;
        _bookingRepo = bookingRepo;
        _homestayService = homestayService;
    }

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

    [Route("booking-homestay")]
    public IActionResult BookingHomestay(int roomId, int businessId)
    {
        var model = _homestayService.GetBookingInfo(roomId, businessId);
        if (model == null) return NotFound();

        return View("~/Views/Home/BookingHomestay.cshtml", model);
    }

    [Route("checkout-homestay")]
    public IActionResult CheckoutHomestay()
    {
        return View("~/Views/Home/CheckoutHomestay.cshtml");
    }

    [Route("confirmation-payment")]
    public IActionResult ConfirmationPayment(string bookingCode)
    {
        if (string.IsNullOrEmpty(bookingCode))
        {
            return RedirectToAction("Index");
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