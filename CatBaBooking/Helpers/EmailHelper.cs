namespace CatBaBooking.Helpers;

/// <summary>
/// Tiện ích gửi email (OTP, xác nhận booking, thông báo...).
/// 
/// [HƯỚNG DẪN] Cài NuGet: MailKit hoặc dùng System.Net.Mail (built-in).
/// Cấu hình SMTP trong appsettings.json:
/// {
///   "Email": {
///     "SmtpHost": "smtp.gmail.com",
///     "SmtpPort": 587,
///     "SenderEmail": "your@gmail.com",
///     "SenderPassword": "app-password",
///     "SenderName": "CatBa Booking"
///   }
/// }
/// </summary>
public class EmailHelper
{
    private readonly IConfiguration _configuration;

    public EmailHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>Gửi email OTP cho chức năng quên mật khẩu.</summary>
    public Task SendOtpEmailAsync(string toEmail, string otpCode)
    {
        // TODO: Tạo MimeMessage với subject "Mã OTP đặt lại mật khẩu"
        //       Body: "Mã OTP của bạn là: {otpCode}. Có hiệu lực trong 5 phút."
        //       Gửi qua SmtpClient
        throw new NotImplementedException();
    }

    /// <summary>Gửi email xác nhận booking thành công.</summary>
    public Task SendBookingConfirmationAsync(string toEmail, int bookingId)
    {
        // TODO: Gửi email với thông tin chi tiết booking
        throw new NotImplementedException();
    }

    /// <summary>Gửi email thông báo booking bị hủy.</summary>
    public Task SendBookingCancellationAsync(string toEmail, int bookingId)
    {
        throw new NotImplementedException();
    }
}
