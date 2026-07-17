using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Auth;

/// <summary>
/// ViewModel cho màn hình Register.
/// </summary>
public class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(100)]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(8, ErrorMessage = "Mật khẩu tối thiểu 8 ký tự")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = "";

    /// <summary>
    /// Loại tài khoản đăng ký: "Customer", "HomestayOwner", "RestaurantOwner".
    /// Dùng để gán RoleId phù hợp khi tạo user.
    /// </summary>
    public string AccountType { get; set; } = "Customer";
}
