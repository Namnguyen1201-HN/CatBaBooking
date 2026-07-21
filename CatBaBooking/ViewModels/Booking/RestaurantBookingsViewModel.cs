namespace CatBaBooking.ViewModels
{
    public class RestaurantBookingsViewModel
    {
        public string RestaurantName { get; set; } = string.Empty;
        public List<BookingListItemViewModel> Bookings { get; set; } = new();

        // Giữ lại giá trị bộ lọc đã chọn để hiển thị lại trên form
        public DateOnly? ReservationDate { get; set; }
        public TimeOnly? ReservationTime { get; set; }
        public int? NumGuests { get; set; }
        public string? Status { get; set; }
        public string? SearchCode { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalCount { get; set; }
    }

    public class BookingListItemViewModel
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string BookerName { get; set; } = string.Empty;
        public string BookerEmail { get; set; } = string.Empty;
        public string BookerPhone { get; set; } = string.Empty;
        public string TableNames { get; set; } = string.Empty;
        public DateOnly? ReservationDate { get; set; }
        public TimeOnly? ReservationTime { get; set; }
        public int NumGuests { get; set; }
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;
        public string StatusLabel { get; set; } = string.Empty;
        public string StatusBadgeClass { get; set; } = string.Empty;

        public string PaymentStatusLabel { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }

        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<BookingDishItemViewModel> Dishes { get; set; } = new();
    }

    public class BookingDishItemViewModel
    {
        public string DishName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceAtBooking { get; set; }
        public string? Notes { get; set; }
    }
}