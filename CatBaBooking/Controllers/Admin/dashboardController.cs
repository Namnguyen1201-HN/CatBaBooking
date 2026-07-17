using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;
[Route("dash-board")]
public class dashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/Dashboard.cshtml");
    }
}