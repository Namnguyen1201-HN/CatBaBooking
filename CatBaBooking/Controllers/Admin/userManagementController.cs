using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;
[Route("user-management")]
public class userManagementController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/UserManagement.cshtml");
    }
}