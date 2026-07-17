namespace CatBaBooking.Helpers;

/// <summary>
/// Tiện ích hash và verify mật khẩu.
/// 
/// [HƯỚNG DẪN] Cài NuGet package BCrypt.Net-Next:
///   Tools → NuGet Package Manager → Manage NuGet → tìm "BCrypt.Net-Next" → Install
/// 
/// Sau đó bỏ comment phần implementation bên dưới.
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// Hash mật khẩu plain-text thành chuỗi hash an toàn.
    /// Lưu chuỗi hash này vào DB, KHÔNG bao giờ lưu plain-text.
    /// </summary>
    public static string Hash(string plainPassword)
    {
        // TODO: return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        throw new NotImplementedException();
    }

    /// <summary>
    /// Xác nhận mật khẩu người dùng nhập có khớp với hash trong DB không.
    /// Dùng khi Login hoặc đổi mật khẩu.
    /// </summary>
    public static bool Verify(string plainPassword, string hashedPassword)
    {
        // TODO: return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        throw new NotImplementedException();
    }
}
