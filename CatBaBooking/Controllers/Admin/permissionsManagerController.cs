using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;
[Route("permissions-manager")]
public class permissionsManagerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/PermissionsManager.cshtml");
    }
}