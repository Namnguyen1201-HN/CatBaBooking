using CatBaBooking.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Owner
{
    public class RestaurantManageTablesController : Controller
    {
        private readonly IRestaurantTableService _tableService;

        public RestaurantManageTablesController(IRestaurantTableService tableService)
        {
            _tableService = tableService;
        }

        private int? GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            return int.TryParse(userIdStr, out var userId) ? userId : null;
        }

        [HttpGet]
        public IActionResult RestaurantManageTables()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var viewModel = _tableService.GetTables(userId.Value);
            return View("~/Views/Owner/RestaurantManageTables.cshtml", viewModel);
        }

        [HttpPost("restaurant-table-add")]
        public IActionResult AddTable(string name, int capacity, bool isActive)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var (success, message) = _tableService.AddTable(userId.Value, name, capacity, isActive);
            TempData[success ? "Message" : "Errors"] = message;
            return RedirectToAction("RestaurantManageTables");
        }

        [HttpPost("restaurant-table-update")]
        public IActionResult UpdateTable(int tableId, string name, int capacity, bool isActive)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var (success, message) = _tableService.UpdateTable(userId.Value, tableId, name, capacity, isActive);
            TempData[success ? "Message" : "Errors"] = message;
            return RedirectToAction("RestaurantManageTables");
        }

        [HttpPost("restaurant-table-delete")]
        public IActionResult DeleteTable(int tableId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Redirect("/login");
            }

            var (success, message) = _tableService.DeleteTable(userId.Value, tableId);
            TempData[success ? "Message" : "Errors"] = message;
            return RedirectToAction("RestaurantManageTables");
        }
    }
}