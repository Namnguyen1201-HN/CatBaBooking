using System;
using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("booking-history")]
public class bookingHistoryController : Controller
{
    private readonly IBookingHistoryService _bookingHistoryService;
    private const int PageSize = 10;

    public bookingHistoryController(IBookingHistoryService bookingHistoryService)
    {
        _bookingHistoryService = bookingHistoryService;
    }

    // GET /booking-history?status=&type=&search=&page=
    public IActionResult Index(string? status, string? type, string? search, int page = 1)
    {
        var result = _bookingHistoryService.GetPagedBookings(status, type, search, page, PageSize);

        ViewBag.Bookings = result.Bookings;
        ViewBag.TotalCount = result.TotalCount;
        ViewBag.CurrentPage = page < 1 ? 1 : page;
        ViewBag.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);

        ViewBag.Status = status;
        ViewBag.Type = type;
        ViewBag.Search = search;

        return View("~/Views/Admin/BookingHistory.cshtml");
    }
}
