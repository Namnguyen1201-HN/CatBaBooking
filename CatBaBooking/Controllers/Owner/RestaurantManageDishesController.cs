using CatBaBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Owner
{
    public class RestaurantManageDishesController : Controller
    {
        private readonly IRestaurantDishService _dishService;
        private readonly IWebHostEnvironment _env;

        public RestaurantManageDishesController(IRestaurantDishService dishService, IWebHostEnvironment env)
        {
            _dishService = dishService;
            _env = env;
        }

        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : null;
        }

        [HttpGet]
        public IActionResult RestaurantManageDishes()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var viewModel = _dishService.GetDishesPageData(userId.Value);
            return View("~/Views/Owner/RestaurantManageDishes.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult AddDish(
            string name, decimal price, int category_id, string? description, bool is_active, IFormFile? dish_image_file)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var imageUrl = SaveDishImage(dish_image_file);

            var (success, message) = _dishService.AddDish(
                userId.Value, name, price, category_id, description, imageUrl, is_active);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction("RestaurantManageDishes");
        }

        [HttpPost]
        public IActionResult UpdateDish(
            int dish_id, string name, decimal price, int category_id, string? description, bool is_active,
            IFormFile? dish_image_file, string? existing_image_url)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            // Có upload ảnh mới thì lưu và dùng ảnh mới; không thì giữ nguyên ảnh cũ (Service sẽ tự bỏ qua nếu null)
            var newImageUrl = SaveDishImage(dish_image_file);

            var (success, message) = _dishService.UpdateDish(
                userId.Value, dish_id, name, price, category_id, description, newImageUrl, is_active);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction("RestaurantManageDishes");
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var (success, message) = _dishService.AddCategory(userId.Value, name);
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = message;
            return RedirectToAction("RestaurantManageDishes");
        }

        private string? SaveDishImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var folder = Path.Combine(_env.WebRootPath, "uploads", "dishes");
            Directory.CreateDirectory(folder);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/uploads/dishes/{fileName}";
        }
    }
}