using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Auth;
using CatBaBooking.Helpers;

namespace CatBaBooking.Service;

public class RegisterCustomerService : IRegisterCustomerService
{
    private readonly IUserRepository _userRepository;

    public RegisterCustomerService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool RegisterCustomer(string email, string password, string fullname, int roleId)
    {
        bool checkemail = _userRepository.AnyEmail(email);
        if (checkemail)
        {
            return false;
        }

        string hashpassword = PasswordHelper.HashPassword(password);

        var newUser = new User(
            0,
            roleId,
            fullname,
            email,
            hashpassword,
            null,
            null,
            null,
            "active",
            DateTime.Now,
            DateTime.Now);

        _userRepository.AddUser(newUser);
        return true;
    }
}
