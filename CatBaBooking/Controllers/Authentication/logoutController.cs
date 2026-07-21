using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Authentication;

[Route("logout")]
public class logoutController : Controller
{
    [HttpGet, HttpPost]
    public IActionResult Index()
    {
        HttpContext.Session.Clear();
        return Redirect("/login");
    }
}