using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("dash-board")]
public class dashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public dashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    // GET
    public IActionResult Index()
    {
        var stats = _dashboardService.GetStats();
        ViewBag.TotalUsers = stats.TotalUsers;
        ViewBag.ActiveBusinesses = stats.ActiveBusinesses;
        ViewBag.MonthlyRevenue = stats.MonthlyRevenue;
        ViewBag.PendingApplications = stats.PendingApplications;
        ViewBag.TotalBookings = stats.TotalBookings;
        ViewBag.PendingPayments = stats.PendingPayments;

        var revenueChart = _dashboardService.GetRevenueChart();
        ViewBag.RevenueLabels = revenueChart.Labels;
        ViewBag.RevenueData = revenueChart.Data;

        var statusCounts = _dashboardService.GetBookingStatusCounts();
        ViewBag.ConfirmedCount = statusCounts.Confirmed;
        ViewBag.PendingCount = statusCounts.Pending;
        ViewBag.CancelledCount = statusCounts.Cancelled;

        var typeChart = _dashboardService.GetTypeChart();
        ViewBag.TypeLabels = typeChart.Labels;
        ViewBag.HomestayData = typeChart.HomestayData;
        ViewBag.RestaurantData = typeChart.RestaurantData;

        ViewBag.RecentBookings = _dashboardService.GetRecentBookings(5);

        return View("~/Views/Admin/Dashboard.cshtml");
    }
}
