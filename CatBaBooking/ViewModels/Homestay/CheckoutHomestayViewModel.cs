using System;
using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Homestay
{
    public class CheckoutHomestayViewModel
    {
        // Trip Details
        public int RoomId { get; set; }
        public int BusinessId { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public int NumGuests { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalNights { get; set; }

        // Room/Homestay Display Info
        public string? RoomName { get; set; }
        public string? HomestayName { get; set; }
        public decimal PricePerNight { get; set; }

        // Contact Info
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string BookerName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string BookerPhone { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string BookerEmail { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
