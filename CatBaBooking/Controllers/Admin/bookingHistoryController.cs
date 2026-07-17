using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;
[Route("booking-history")]
public class bookingHistoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/BookingHistory.cshtml");
    }
}