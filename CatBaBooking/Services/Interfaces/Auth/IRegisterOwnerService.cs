using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Auth;

public interface IRegisterOwnerService
{
    User RegisterOwner(string email, string password, string fullname, int roleId, string address, string citizen, string phone);
}
