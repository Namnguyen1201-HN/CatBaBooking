using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Auth;
using CatBaBooking.Helpers;

namespace CatBaBooking.Service;

public class ForgotPasswordService : IForgotPasswordService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ForgotPasswordService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }       

    public string? SendOTP(string email)
    {
        var user = _userRepository.GetActiveUserByEmail(email);
        if (user == null) return null;

        string otp = new Random().Next(100000, 999999).ToString();
        _emailService.Email(email, otp);
        return otp;
    }

    public bool ResetPassword(string email, string newPassword)
    {
        var user = _userRepository.GetByEmail(email);
        if (user == null) return false;

        user.PasswordHash = PasswordHelper.HashPassword(newPassword);
        user.UpdatedAt = DateTime.Now;

        _userRepository.UpdateUser(user);
        return true;
    }
}
