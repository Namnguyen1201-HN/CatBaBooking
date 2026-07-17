using CatBaBooking.Service.Interface.Auth;
using CatBaBooking.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Authentication;

[Route("register-owner")]
public class registerOwnerController : Controller
{
    private readonly IRegisterOwnerService registerOwnerService;
    private readonly IRegisterBusinessService registerBusinessService;

    public registerOwnerController(IRegisterOwnerService registerOwnerService,
        IRegisterBusinessService registerBusinessService)
    {
        this.registerOwnerService = registerOwnerService;
        this.registerBusinessService = registerBusinessService;
    }
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Authentication/RegisterOwner.cshtml");
    }

    [HttpPost]
    public IActionResult RegisterOwner(string email, string password, string confirmPassword, string fullname, int roleId,
        string address, string citizen, string phone, string name, string addressBusiness, int ownerId, string type, string description)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullname) ||
            string.IsNullOrEmpty(address) || string.IsNullOrEmpty(citizen) || string.IsNullOrEmpty(phone) ||
            string.IsNullOrEmpty(name) || string.IsNullOrEmpty(addressBusiness) || string.IsNullOrEmpty(type) ||
            string.IsNullOrEmpty(description) || string.IsNullOrEmpty(confirmPassword))
        {
            ViewBag.Error = "Không được để trống các trường";
            return View("~/Views/Authentication/RegisterOwner.cshtml");
        }

        if (!password.Equals(confirmPassword))
        {
            ViewBag.Error = "Mật khẩu và xác nhận mật khẩu không khớp";
            return View("~/Views/Authentication/RegisterOwner.cshtml");
        }

        if (!PasswordHelper.IsStrongPassword(password))
        {
            ViewBag.Error =
                "Mật khẩu không hợp lệ! Yêu cầu: Độ dài từ 8 ký tự trở lên, phải bao gồm chữ hoa (A-Z), chữ thường (a-z), chữ số (0-9) và ký tự đặc biệt (như @, $, !, %, *, ?, &).";
            return View("~/Views/Authentication/RegisterOwner.cshtml");
        }

        switch (type)
        {
            case "homestay":
                roleId = 2;
                break;
            case "restaurant":
                roleId = 4;
                break;
            default:
                ViewBag.Error = "Loại hình Business không hợp lệ";
                return View("~/Views/Authentication/RegisterOwner.cshtml");
        }

        var user =
            registerOwnerService.RegisterOwner(email, password, fullname, roleId, address, citizen,
                phone);
        if (user == null)
        {
            ViewBag.Error = "Đăng kí tài khoản thất bại";
            return View("~/Views/Authentication/RegisterOwner.cshtml");
        }
        bool isSuccessBusiness = registerBusinessService.RegisterBusiness(name, addressBusiness, user.UserId, type, description);
        if (!isSuccessBusiness)
        {
            ViewBag.Error = "Đăng kí cơ sở thất bại";
            return View("~/Views/Authentication/RegisterOwner.cshtml");
        }

        TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Vui lòng đợi hệ thống duyệt.";
        return Redirect("/login");
    }
}
