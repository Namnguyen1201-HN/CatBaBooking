namespace CatBaBooking.ViewModels.Homestay;

/// <summary>
/// ViewModel cho trang chi tiết Homestay.
/// Gom thông tin từ nhiều bảng: Business + Rooms + Reviews + Amenities.
/// </summary>
public class HomestayDetailViewModel
{
    // ── Thông tin cơ bản ──────────────────────────────────────────────────────
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? AreaName { get; set; }
    public string? Phone { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? OwnerName { get; set; }

    // ── Tiện nghi (Amenities) ─────────────────────────────────────────────────
    public IEnumerable<string> Amenities { get; set; } = new List<string>();

    // ── Danh sách phòng ───────────────────────────────────────────────────────
    public IEnumerable<RoomSummaryViewModel> Rooms { get; set; } = new List<RoomSummaryViewModel>();

    // ── Đánh giá ──────────────────────────────────────────────────────────────
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public IEnumerable<ReviewSummaryViewModel> RecentReviews { get; set; } = new List<ReviewSummaryViewModel>();
}

/// <summary>Thông tin tóm tắt một phòng — hiển thị trong trang detail Homestay.</summary>
public class RoomSummaryViewModel
{
    public int RoomId { get; set; }
    public string? RoomName { get; set; }
    public string? RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsAvailable { get; set; }
}

/// <summary>Thông tin tóm tắt một review — dùng trong trang detail.</summary>
public class ReviewSummaryViewModel
{
    public string UserName { get; set; } = "";
    public string? UserAvatarUrl { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
