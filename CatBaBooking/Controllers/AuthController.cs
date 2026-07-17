using CatBaBooking.Helpers;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers;

/// <summary>
/// Xử lý tất cả chức năng xác thực: Login, Logout, Register, ForgotPassword.
/// 
/// [HƯỚNG DẪN LUỒNG CONTROLLER]
/// 1. Action GET  → trả về View (hiển thị form trống)
/// 2. Action POST → nhận data từ form → validate → gọi Service → redirect
/// 
/// Controller KHÔNG chứa business logic:
///   - Không kết nối DB trực tiếp
///   - Không hash password
///   - Không validate nghiệp vụ (email trùng, password sai...)
///   → Tất cả do Service lo
/// </summary>
public class AuthController : Controller
{
    // Service được inject qua constructor (DI)
    // Đăng ký trong Program.cs: builder.Services.AddScoped<IUserService, UserService>()
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    // ── LOGIN ─────────────────────────────────────────────────────────────────

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        // Nếu đã login rồi → redirect về trang chủ
        // TODO: if (SessionHelper.IsAuthenticated(HttpContext.Session)) return RedirectToAction("HomePage", "Home");
        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // TODO:
        // 1. Kiểm tra form hợp lệ: if (!ModelState.IsValid) return View(model);
        // 2. Gọi service: var user = await _userService.LoginAsync(model.Email, model.Password);
        // 3. Nếu user == null: ModelState.AddModelError("", "Email hoặc mật khẩu không đúng"); return View(model);
        // 4. Lưu session: SessionHelper.SetCurrentUser(HttpContext.Session, user);
        // 5. Redirect theo role: Admin → /admin, Owner → /owner, Customer → /
        throw new NotImplementedException();
    }

    // ── LOGOUT ────────────────────────────────────────────────────────────────

    [Route("logout")]
    public IActionResult Logout()
    {
        // TODO: SessionHelper.ClearSession(HttpContext.Session);
        // TODO: return RedirectToAction("HomePage", "Home");
        throw new NotImplementedException();
    }

    // ── REGISTER ──────────────────────────────────────────────────────────────

    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // TODO:
        // 1. if (!ModelState.IsValid) return View(model);
        // 2. var result = await _userService.RegisterAsync(model);
        // 3. if (!result.Success) { ModelState.AddModelError("", result.ErrorMessage); return View(model); }
        // 4. TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
        // 5. return RedirectToAction("Login");
        throw new NotImplementedException();
    }

    // ── FORGOT PASSWORD ───────────────────────────────────────────────────────

    [HttpGet]
    [Route("forgot-password")]
    public IActionResult ForgotPassword() => View();

    [HttpPost]
    [Route("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        // TODO: Gọi EmailHelper.SendOtpEmailAsync → redirect sang trang nhập OTP
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("verify-otp")]
    public IActionResult VerifyOtp() => View();

    [HttpPost]
    [Route("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpViewModel model)
    {
        // TODO: Xác nhận OTP → redirect sang trang đặt mật khẩu mới
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("reset-password")]
    public IActionResult ResetPassword() => View();

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        // TODO: Gọi _userService.ChangePasswordAsync → redirect Login
        throw new NotImplementedException();
    }
}
