using CatBaBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Owner
{
    public class OwnerDashboardController : Controller
    {
        private readonly IOwnerDashboardService _dashboardService;

        public OwnerDashboardController(IOwnerDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : null;
        }

        public IActionResult OwnerDashboard()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var viewModel = _dashboardService.GetDashboardData(userId.Value);

            return View("~/Views/Owner/OwnerDashboard.cshtml", viewModel);
        }
    }
}