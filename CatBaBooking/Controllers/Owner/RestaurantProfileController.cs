using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Owner
{
    public class RestaurantProfileController : Controller
    {
        private readonly IRestaurantProfileService _profileService;

        public RestaurantProfileController(IRestaurantProfileService profileService)
        {
            _profileService = profileService;
        }

        // Session lưu UserId bằng SetString (xem loginController.cs) nên phải đọc bằng GetString + int.Parse,
        // KHÔNG dùng GetInt32 (2 định dạng lưu trữ khác nhau, không tương thích).
        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : null;
        }

        [HttpGet]
        public IActionResult RestaurantProfile()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var viewModel = _profileService.GetProfile(userId.Value);

            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "Bạn chưa có nhà hàng nào để chỉnh sửa.";
                viewModel = new RestaurantProfileViewModel();
            }

            return View("~/Views/Owner/RestaurantProfile.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult UpdateProfile(RestaurantProfileViewModel model)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var (success, message) = _profileService.UpdateProfile(userId.Value, model);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction("RestaurantProfile");
        }
    }
}