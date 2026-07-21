using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class checkoutRestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public checkoutRestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [Route("checkout-restaurant")]
        [HttpGet]
        public IActionResult CheckoutRestaurant(int businessId = 1)
        {
            int? userId = null;
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int parsedId))
            {
                userId = parsedId;
            }

            List<CartItemViewModel> guestCartItems = null;
            if (!userId.HasValue)
            {
                var sessionKey = $"GuestCart_{businessId}";
                var json = HttpContext.Session.GetString(sessionKey);
                if (!string.IsNullOrEmpty(json))
                {
                    var items = JsonSerializer.Deserialize<List<CartItemRequest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (items != null)
                    {
                        guestCartItems = items.Select(i => new CartItemViewModel
                        {
                            DishId = i.Id,
                            DishName = i.Name,
                            Price = i.Price,
                            Quantity = i.Qty,
                            ImageUrl = i.Img
                        }).ToList();
                    }
                }
            }

            var model = _restaurantService.GetCheckoutInfo(businessId, userId, guestCartItems);
            if (model == null) return NotFound();

            // Default values
            model.ReservationDate = DateOnly.FromDateTime(DateTime.Now);
            model.ReservationTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2));
            model.NumGuests = 2;

            return View("~/Views/Home/CheckoutRestaurant.cshtml", model);
        }

        [Route("checkout-restaurant")]
        [HttpPost]
        public IActionResult CheckoutRestaurant(CheckoutRestaurantViewModel model)
        {
            int? userId = null;
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int parsedId))
            {
                userId = parsedId;
            }

            List<CartItemViewModel> guestCartItems = null;
            if (!userId.HasValue)
            {
                var sessionKey = $"GuestCart_{model.BusinessId}";
                var json = HttpContext.Session.GetString(sessionKey);
                if (!string.IsNullOrEmpty(json))
                {
                    var items = JsonSerializer.Deserialize<List<CartItemRequest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (items != null)
                    {
                        guestCartItems = items.Select(i => new CartItemViewModel
                        {
                            DishId = i.Id,
                            DishName = i.Name,
                            Price = i.Price,
                            Quantity = i.Qty,
                            ImageUrl = i.Img
                        }).ToList();
                    }
                }
            }

            ModelState.Remove("RestaurantName");
            ModelState.Remove("RestaurantAddress");

            if (!ModelState.IsValid)
            {
                var checkoutInfo = _restaurantService.GetCheckoutInfo(model.BusinessId, userId, guestCartItems);
                if (checkoutInfo != null)
                {
                    model.CartItems = checkoutInfo.CartItems;
                    model.TotalAmount = checkoutInfo.TotalAmount;
                    model.RestaurantName = checkoutInfo.RestaurantName;
                    model.RestaurantAddress = checkoutInfo.RestaurantAddress;
                }
                return View("~/Views/Home/CheckoutRestaurant.cshtml", model);
            }

            var bookingCode = _restaurantService.PlaceBooking(model, userId, guestCartItems);
            if (bookingCode != null)
            {
                if (!userId.HasValue)
                {
                    HttpContext.Session.Remove($"GuestCart_{model.BusinessId}");
                }
                return RedirectToAction("ConfirmationPayment", "confirmationPayment", new { bookingCode = bookingCode });
            }

            ModelState.AddModelError("", "Đặt bàn thất bại. Vui lòng thử lại.");
            var fallbackInfo = _restaurantService.GetCheckoutInfo(model.BusinessId, userId, guestCartItems);
            if (fallbackInfo != null)
            {
                model.CartItems = fallbackInfo.CartItems;
                model.TotalAmount = fallbackInfo.TotalAmount;
            }
            return View("~/Views/Home/CheckoutRestaurant.cshtml", model);
        }
    }
}
