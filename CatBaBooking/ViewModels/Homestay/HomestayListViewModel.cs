namespace CatBaBooking.ViewModels.Homestay;

/// <summary>
/// Thông tin tóm tắt một Homestay — hiển thị dạng card trong trang listing.
/// Map từ entity Business (BusinessType = "Homestay").
/// 
/// [Tại sao không dùng thẳng entity Business?]
///   - Business entity có 20+ fields, card chỉ cần 6-7 fields
///   - Tránh lộ thông tin nhạy cảm (OwnerId, Status nội bộ...)
///   - Dễ thêm computed field: AverageRating, PriceFrom không có trong entity
/// </summary>
public class HomestayCardViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? ThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public string? AreaName { get; set; }

    /// <summary>Giá phòng rẻ nhất — tính từ danh sách Rooms.</summary>
    public decimal PriceFrom { get; set; }

    /// <summary>Điểm đánh giá trung bình — tính từ danh sách Reviews.</summary>
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}

/// <summary>
/// ViewModel cho trang danh sách Homestay (bao gồm phân trang).
/// </summary>
public class HomestayListViewModel
{
    public IEnumerable<HomestayCardViewModel> Items { get; set; } = new List<HomestayCardViewModel>();

    // ── Phân trang ────────────────────────────────────────────────────────────
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
