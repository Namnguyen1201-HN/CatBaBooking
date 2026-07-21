namespace CatBaBooking.Repository.Interface;

// DTO gộp dữ liệu Booking + Bàn đã đặt + Món đặt trước + Thanh toán gần nhất.
// Đây là dữ liệu "thô" ở tầng Repository, Service sẽ map sang ViewModel để hiển thị.
public class BookingDetailDto
{
    public int BookingId { get; set; }
    public string BookingCode { get; set; } = string.Empty;
    public string BookerName { get; set; } = string.Empty;
    public string BookerEmail { get; set; } = string.Empty;
    public string BookerPhone { get; set; } = string.Empty;
    public int NumGuests { get; set; }
    public decimal TotalPrice { get; set; }
    public string? PaymentStatus { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
    public TimeOnly? ReservationTime { get; set; }
    public DateOnly? ReservationDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<string> TableNames { get; set; } = new();
    public List<BookingDishDto> Dishes { get; set; } = new();
    public string? LastPaymentMethod { get; set; }
}

public class BookingDishDto
{
    public string DishName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PriceAtBooking { get; set; }
    public string? Notes { get; set; }
}