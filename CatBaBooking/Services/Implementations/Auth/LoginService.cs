using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Auth;
using CatBaBooking.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Service;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);

        if (user == null || user.Status != "active") return null;

        bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.PasswordHash);
        if (!isPasswordValid) return null;

        return user;
    }
}
