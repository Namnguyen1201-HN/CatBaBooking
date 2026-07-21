using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class cartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public cartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpPost]
        [Route("cart/save")]
        public IActionResult SaveCart([FromBody] SaveCartRequest request)
        {
            if (request == null || request.Items == null)
            {
                return BadRequest("Invalid cart data.");
            }

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                // Customer -> Save to DB
                var tempCartItems = request.Items.Select(i => new TempCart
                {
                    UserId = userId,
                    BusinessId = request.BusinessId,
                    DishId = i.Id,
                    Quantity = i.Qty,
                    Subtotal = i.Qty * i.Price,
                    CreatedAt = DateTime.Now
                }).ToList();

                _cartRepo.SaveCartItems(request.BusinessId, userId, tempCartItems);
            }
            else
            {
                // Guest -> Save to Session
                var sessionKey = $"GuestCart_{request.BusinessId}";
                var json = JsonSerializer.Serialize(request.Items);
                HttpContext.Session.SetString(sessionKey, json);
            }

            return Json(new { success = true });
        }
    }

    public class SaveCartRequest
    {
        public int BusinessId { get; set; }
        public List<CartItemRequest> Items { get; set; }
    }

    public class CartItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
        public int Qty { get; set; }
    }
}
