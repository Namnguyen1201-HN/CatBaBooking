using CatBaBooking.Models;
using CatBaBooking.ViewModels.Auth;

namespace CatBaBooking.Services.Interfaces;

/// <summary>
/// Tầng Service xử lý business logic liên quan đến User.
/// 
/// [KHÁC BIỆT với Repository]
///   - Repository chỉ biết CRUD với DB
///   - Service biết NGHIỆP VỤ: validate input, hash password, kiểm tra điều kiện...
///   - Controller chỉ gọi Service, KHÔNG gọi Repository trực tiếp
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Xác thực đăng nhập.
    /// Trả về User nếu email + password đúng, null nếu sai.
    /// [Service sẽ: tìm user theo email → verify hash password → trả về user]
    /// </summary>
    Task<User?> LoginAsync(string email, string password);

    /// <summary>
    /// Đăng ký tài khoản mới.
    /// [Service sẽ: kiểm tra email trùng → hash password → tạo user với role mặc định]
    /// </summary>
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model);

    /// <summary>Lấy thông tin user theo ID.</summary>
    Task<User?> GetProfileAsync(int userId);

    /// <summary>Cập nhật profile (tên, phone, avatar).</summary>
    Task<bool> UpdateProfileAsync(int userId, string fullName, string phone, string? avatarUrl);

    /// <summary>
    /// Đổi mật khẩu.
    /// [Service sẽ: verify mật khẩu cũ → hash mật khẩu mới → update]
    /// </summary>
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);

    // ── ADMIN ─────────────────────────────────────────────────────────────────

    /// <summary>Lấy danh sách tất cả user (Admin).</summary>
    Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>Khóa / mở khóa tài khoản (Admin).</summary>
    Task<bool> UpdateUserStatusAsync(int userId, string status);
}
