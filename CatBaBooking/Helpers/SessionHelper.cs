using CatBaBooking.Models;
using System.Text.Json;

namespace CatBaBooking.Helpers;

/// <summary>
/// Tiện ích đọc/ghi thông tin người dùng trong Session.
/// 
/// [HƯỚNG DẪN] Dùng HttpContext.Session để lưu thông tin user sau khi Login.
/// Session hoạt động như một "dictionary" lưu key-value theo phiên làm việc.
/// 
/// [SETUP] Cần thêm trong Program.cs:
///   builder.Services.AddSession();
///   builder.Services.AddHttpContextAccessor();
///   app.UseSession();  // đặt TRƯỚC UseRouting
/// </summary>
public static class SessionHelper
{
    private const string UserSessionKey = "CurrentUser";

    /// <summary>
    /// Lưu thông tin user vào Session sau khi Login thành công.
    /// Serialize thành JSON vì Session chỉ lưu được string/bytes.
    /// </summary>
    public static void SetCurrentUser(ISession session, User user)
    {
        // TODO: session.SetString(UserSessionKey, JsonSerializer.Serialize(user));
        throw new NotImplementedException();
    }

    /// <summary>
    /// Lấy thông tin user hiện đang đăng nhập từ Session.
    /// Trả về null nếu chưa đăng nhập.
    /// </summary>
    public static User? GetCurrentUser(ISession session)
    {
        // TODO:
        // var json = session.GetString(UserSessionKey);
        // return json == null ? null : JsonSerializer.Deserialize<User>(json);
        throw new NotImplementedException();
    }

    /// <summary>Xóa session khi Logout.</summary>
    public static void ClearSession(ISession session)
    {
        // TODO: session.Remove(UserSessionKey);
        throw new NotImplementedException();
    }

    /// <summary>Kiểm tra người dùng hiện tại có role cụ thể không.</summary>
    public static bool IsInRole(ISession session, string roleName)
    {
        // TODO: return GetCurrentUser(session)?.Role?.RoleName == roleName;
        throw new NotImplementedException();
    }

    /// <summary>Kiểm tra đã đăng nhập chưa.</summary>
    public static bool IsAuthenticated(ISession session)
    {
        // TODO: return GetCurrentUser(session) != null;
        throw new NotImplementedException();
    }
}
