using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Business.
/// Business là entity cha chứa cả Homestay lẫn Restaurant.
/// </summary>
public interface IBusinessRepository
{
    // ── READ ──────────────────────────────────────────────────────────────────

    /// <summary>Lấy tất cả business đang active (cho trang listing).</summary>
    Task<IEnumerable<Business>> GetAllActiveAsync();

    /// <summary>Lấy chi tiết một business theo ID, kèm theo các navigation property.</summary>
    Task<Business?> GetByIdAsync(int businessId);

    /// <summary>Lấy danh sách business theo Owner (dùng cho Owner Dashboard).</summary>
    Task<IEnumerable<Business>> GetByOwnerIdAsync(int ownerId);

    /// <summary>Lấy danh sách business đang chờ phê duyệt (dùng cho Admin).</summary>
    Task<IEnumerable<Business>> GetPendingAsync();

    /// <summary>Lấy danh sách Homestay (type = 'Homestay') với phân trang.</summary>
    Task<IEnumerable<Business>> GetHomestaysAsync(int page, int pageSize);

    /// <summary>Lấy danh sách Restaurant (type = 'Restaurant') với phân trang.</summary>
    Task<IEnumerable<Business>> GetRestaurantsAsync(int page, int pageSize);

    /// <summary>Đếm tổng số homestay (dùng để tính pagination).</summary>
    Task<int> CountHomestaysAsync();

    /// <summary>Đếm tổng số restaurant.</summary>
    Task<int> CountRestaurantsAsync();

    // ── CREATE ────────────────────────────────────────────────────────────────

    /// <summary>Thêm business mới (Owner đăng ký listing mới).</summary>
    Task<Business> CreateAsync(Business business);

    // ── UPDATE ────────────────────────────────────────────────────────────────

    /// <summary>Cập nhật thông tin business.</summary>
    Task UpdateAsync(Business business);

    /// <summary>Cập nhật trạng thái phê duyệt (approved / rejected / pending) — Admin.</summary>
    Task UpdateStatusAsync(int businessId, string status);

    // ── DELETE ────────────────────────────────────────────────────────────────

    /// <summary>Xóa business (Admin hoặc chính Owner).</summary>
    Task DeleteAsync(int businessId);
}
