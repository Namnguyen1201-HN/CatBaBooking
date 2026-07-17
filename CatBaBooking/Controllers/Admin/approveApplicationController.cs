using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("approve-application")]
public class approveApplicationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/Approveapplication.cshtml");
    }
}