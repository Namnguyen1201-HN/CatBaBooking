using CatBaBooking.Helpers;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Auth;

namespace CatBaBooking.Service;

public class RegisterOwnerService : IRegisterOwnerService
{
    private readonly IUserRepository _userRepository;

    public RegisterOwnerService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? RegisterOwner(string email, string password, string fullname, int roleId, string address, string citizen, string phone)
    {
        bool checkEmail = _userRepository.AnyEmail(email);
        if (checkEmail) return null;

        var newUser = new User(
    0, roleId, fullname, email,
    PasswordHelper.HashPassword(password),
    phone, citizen, address, "pending", DateTime.Now, DateTime.Now);

        return _userRepository.AddUser(newUser);
    }
}
