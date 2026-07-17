namespace CatBaBooking.ViewModels.Booking;

/// <summary>Hiển thị một booking trong lịch sử đặt chỗ.</summary>
public class BookingHistoryViewModel
{
    public int BookingId { get; set; }
    public string BusinessName { get; set; } = "";
    public string? BusinessThumbnail { get; set; }
    public string BookingType { get; set; } = "";  // "Homestay" | "Restaurant"
    public DateTime BookingDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "";  // pending | confirmed | cancelled | completed
}

/// <summary>Chi tiết một booking cụ thể.</summary>
public class BookingDetailViewModel
{
    public int BookingId { get; set; }
    public string BusinessName { get; set; } = "";
    public string BookingType { get; set; } = "";
    public DateTime BookingDate { get; set; }
    public string Status { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public string? SpecialRequests { get; set; }

    // Homestay
    public List<BookedRoomInfo> BookedRooms { get; set; } = new();

    // Restaurant
    public List<BookedTableInfo> BookedTables { get; set; } = new();
    public List<BookedDishInfo> BookedDishes { get; set; } = new();

    // Payment
    public string? PaymentStatus { get; set; }
    public string? PaymentMethod { get; set; }
}

public class BookedRoomInfo
{
    public string? RoomName { get; set; }
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public decimal Price { get; set; }
}

public class BookedTableInfo
{
    public string? TableName { get; set; }
    public DateOnly ReservationDate { get; set; }
    public TimeOnly ReservationTime { get; set; }
}

public class BookedDishInfo
{
    public string? DishName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
