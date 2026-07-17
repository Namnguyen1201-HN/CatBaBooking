namespace CatBaBooking.Service.Interface.Auth;

public interface IEmailService
{
    bool Email(string email, string otp);
}
