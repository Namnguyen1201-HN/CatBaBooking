using CatBaBooking.Models;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Interfaces;

/// <summary>
/// Tầng Service xử lý business logic cho Business (Homestay + Restaurant).
/// Service này map giữa entity Business và các ViewModel để trả về View.
/// </summary>
public interface IBusinessService
{
    // ── CUSTOMER / GUEST (Browse & Search) ────────────────────────────────────

    /// <summary>
    /// Lấy danh sách homestay với phân trang, trả về ViewModel (không phải entity).
    /// [Service sẽ: gọi Repo → map Business → HomestayCardViewModel]
    /// </summary>
    Task<HomestayListViewModel> GetHomestayListAsync(int page, int pageSize);

    /// <summary>Lấy chi tiết homestay, gồm rooms và reviews.</summary>
    Task<HomestayDetailViewModel?> GetHomestayDetailAsync(int businessId);

    /// <summary>Lấy danh sách restaurant với phân trang.</summary>
    Task<RestaurantListViewModel> GetRestaurantListAsync(int page, int pageSize);

    /// <summary>Lấy chi tiết restaurant, gồm tables, dishes và reviews.</summary>
    Task<RestaurantDetailViewModel?> GetRestaurantDetailAsync(int businessId);

    // ── OWNER (Manage Listings) ────────────────────────────────────────────────

    /// <summary>Lấy danh sách business của một Owner.</summary>
    Task<IEnumerable<Business>> GetOwnerBusinessesAsync(int ownerId);

    /// <summary>Thêm listing mới (Owner).</summary>
    Task<Business> CreateBusinessAsync(Business business);

    /// <summary>Cập nhật thông tin listing (Owner).</summary>
    Task<bool> UpdateBusinessAsync(Business business, int ownerId);

    /// <summary>Xóa listing (Owner hoặc Admin).</summary>
    Task<bool> DeleteBusinessAsync(int businessId, int requesterId);

    // ── ADMIN ─────────────────────────────────────────────────────────────────

    /// <summary>Lấy danh sách listing chờ phê duyệt.</summary>
    Task<IEnumerable<Business>> GetPendingListingsAsync();

    /// <summary>Phê duyệt hoặc từ chối listing.</summary>
    Task<bool> UpdateListingStatusAsync(int businessId, string status);
}
