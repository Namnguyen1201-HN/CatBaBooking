using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Auth;

public interface IRegisterCustomerService
{
    bool RegisterCustomer(string email, string password, string fullname, int roleId);
}
