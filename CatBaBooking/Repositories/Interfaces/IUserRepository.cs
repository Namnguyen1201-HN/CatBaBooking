using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho User.
/// Controller sẽ KHÔNG dùng interface này trực tiếp — chỉ Service mới dùng.
/// </summary>
public interface IUserRepository
{
    // ── READ ──────────────────────────────────────────────────────────────────

    /// <summary>Lấy user theo ID. Trả về null nếu không tìm thấy.</summary>
    Task<User?> GetByIdAsync(int userId);

    /// <summary>Lấy user theo email (dùng cho Login, ForgotPassword).</summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>Lấy danh sách tất cả user (dùng cho Admin).</summary>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>Kiểm tra email đã tồn tại chưa (dùng khi Register).</summary>
    Task<bool> EmailExistsAsync(string email);

    // ── CREATE ────────────────────────────────────────────────────────────────

    /// <summary>Thêm user mới vào DB (dùng khi Register).</summary>
    Task<User> CreateAsync(User user);

    // ── UPDATE ────────────────────────────────────────────────────────────────

    /// <summary>Cập nhật thông tin profile (tên, phone, avatar).</summary>
    Task UpdateAsync(User user);

    /// <summary>Cập nhật trạng thái tài khoản (active / banned) — dùng cho Admin.</summary>
    Task UpdateStatusAsync(int userId, string status);

    /// <summary>Đổi mật khẩu (sau khi đã hash).</summary>
    Task UpdatePasswordAsync(int userId, string hashedPassword);
}
