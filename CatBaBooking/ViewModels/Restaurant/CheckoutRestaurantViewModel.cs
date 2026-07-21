using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Restaurant
{
    public class CheckoutRestaurantViewModel
    {
        public int BusinessId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string RestaurantAddress { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(100)]
        public string BookerName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(20)]
        public string BookerPhone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string BookerEmail { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn ngày đặt bàn")]
        public DateOnly ReservationDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giờ đặt bàn")]
        public TimeOnly ReservationTime { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng khách")]
        [Range(1, 100, ErrorMessage = "Số lượng khách từ 1-100 người")]
        public int NumGuests { get; set; }

        [StringLength(500)]
        public string? SpecialRequests { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "cash";

        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public decimal TotalAmount { get; set; }
    }

    public class CartItemViewModel
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal => Quantity * Price;
    }
}
