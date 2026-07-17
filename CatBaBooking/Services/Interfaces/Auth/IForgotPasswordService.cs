namespace CatBaBooking.Service.Interface.Auth;

public interface IForgotPasswordService
{
    string SendOTP(string email);
    bool ResetPassword(string email, string newPassword);
}
