using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Booking;

/// <summary>
/// ViewModel cho form đặt chỗ (dùng chung cho cả Homestay và Restaurant).
/// Controller sẽ nhận model này từ form → gọi BookingService.
/// </summary>
public class BookingFormViewModel
{
    public int BusinessId { get; set; }

    /// <summary>Loại booking: "Homestay" hoặc "Restaurant".</summary>
    public string BookingType { get; set; } = "";

    // ── Dành cho Homestay ─────────────────────────────────────────────────────
    public List<int> SelectedRoomIds { get; set; } = new();

    [DataType(DataType.Date)]
    public DateOnly? CheckIn { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? CheckOut { get; set; }

    // ── Dành cho Restaurant ───────────────────────────────────────────────────
    public List<int> SelectedTableIds { get; set; } = new();

    /// <summary>Các món ăn đã chọn: key = DishId, value = số lượng.</summary>
    public Dictionary<int, int> SelectedDishes { get; set; } = new();

    [DataType(DataType.Date)]
    public DateOnly? ReservationDate { get; set; }

    [DataType(DataType.Time)]
    public TimeOnly? ReservationTime { get; set; }

    // ── Thông tin chung ───────────────────────────────────────────────────────
    [Required]
    [StringLength(500)]
    public string? SpecialRequests { get; set; }

    /// <summary>Tổng tiền tạm tính (hiển thị UI, xác thực lại ở Service).</summary>
    public decimal EstimatedTotal { get; set; }
}
