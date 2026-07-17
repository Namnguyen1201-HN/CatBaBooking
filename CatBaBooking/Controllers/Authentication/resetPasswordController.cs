using CatBaBooking.Service.Interface.Auth;
using CatBaBooking.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Authentication;
[Route("reset-password")]
public class resetPasswordController : Controller
{
    private readonly IForgotPasswordService forgotPasswordService;

    public resetPasswordController(IForgotPasswordService forgotPasswordService)
    {
        this.forgotPasswordService = forgotPasswordService;
    }
    // GET
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("IsVerify") != "true")
        {
            return Redirect("/forgot-password");
        }
        return View("~/Views/Authentication/ResetPassword.cshtml");
    }

    [HttpPost]
    public IActionResult ResetPassword(string newPassword, string confirmPassword)
    {
        if (HttpContext.Session.GetString("IsVerify") != "true")
        {
            return Redirect("/forgot-password");
        }
        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            ViewBag.Error = "Không được để trống các trường";
            return View("~/Views/Authentication/ResetPassword.cshtml");
        }

        if (!newPassword.Equals(confirmPassword))
        {
            ViewBag.Error = "Mật khẩu mới và xác nhận mật khẩu không khớp nhau";
            return View("~/Views/Authentication/ResetPassword.cshtml");
        }

        if (!PasswordHelper.IsStrongPassword(newPassword))
        {
            ViewBag.Error = "Mật khẩu không hợp lệ! Yêu cầu: Độ dài từ 8 ký tự trở lên, phải bao gồm chữ hoa (A-Z), chữ thường (a-z), chữ số (0-9) và ký tự đặc biệt (như @, $, !, %, *, ?, &).";
            return View("~/Views/Authentication/ResetPassword.cshtml");
        }

        string email = HttpContext.Session.GetString("EmailForgot");
        bool isSuccess = forgotPasswordService.ResetPassword(email, newPassword);
        if (!isSuccess)
        {
            ViewBag.Error = "Đã xảy ra lỗi trong quá trình thực hiện. Vui lòng thử lại.";
            return View("~/Views/Authentication/ResetPassword.cshtml");
        }
        HttpContext.Session.Clear();
        TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công! Mời bạn đăng nhập.";
        return Redirect("/login");
    }
}
