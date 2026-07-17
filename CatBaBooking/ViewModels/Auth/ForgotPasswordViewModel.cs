using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Auth;

/// <summary>ViewModel cho màn hình quên mật khẩu.</summary>
public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress]
    public string Email { get; set; } = "";
}

/// <summary>ViewModel cho bước nhập OTP xác nhận.</summary>
public class VerifyOtpViewModel
{
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Vui lòng nhập mã OTP")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP gồm 6 chữ số")]
    public string Otp { get; set; } = "";
}

/// <summary>ViewModel cho bước nhập mật khẩu mới.</summary>
public class ResetPasswordViewModel
{
    public string Email { get; set; } = "";
    public string Token { get; set; } = "";  // Token xác nhận từ email

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = "";

    [Compare("NewPassword")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = "";
}
