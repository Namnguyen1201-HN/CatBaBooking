using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("feedback-management")]
public class feedbackManagementController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Admin/FeedbackManagement.cshtml");
    }
}