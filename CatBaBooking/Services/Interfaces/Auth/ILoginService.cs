using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Auth;

public interface ILoginService
{
    User Login(string email, string password);
}
