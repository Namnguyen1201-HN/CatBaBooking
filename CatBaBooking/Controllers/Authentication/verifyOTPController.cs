using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Authentication;
[Route("verify-otp")]
public class verifyOTPController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Authentication/VerifyOTP.cshtml");
    }

    [HttpPost]
    public IActionResult Verify(string otp)
    {
        string sessionOTP = HttpContext.Session.GetString("OTP");
        string time = HttpContext.Session.GetString("Timeout");
        if (string.IsNullOrEmpty(otp) || DateTime.Now.Ticks > long.Parse(time))
        {
            ViewBag.Error = "Vui lòng nhập mã OTP";
            return View("~/Views/Authentication/VerifyOTP.cshtml");
        }

        if (sessionOTP == null || !sessionOTP.Equals(otp.Trim()))
        {
            ViewBag.Error = "Mã OTP không chính xác";
            return View("~/Views/Authentication/VerifyOTP.cshtml");
        }
        HttpContext.Session.SetString("IsVerify","true");
        return Redirect("/reset-password");
    }
}