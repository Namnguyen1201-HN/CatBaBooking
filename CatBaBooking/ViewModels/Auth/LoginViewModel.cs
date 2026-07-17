using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Auth;

/// <summary>
/// ViewModel cho màn hình Login.
/// DataAnnotations dùng để validate form phía server (ModelState).
/// </summary>
public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    /// <summary>Checkbox "Ghi nhớ đăng nhập" → lưu cookie lâu hơn.</summary>
    public bool RememberMe { get; set; }
}
