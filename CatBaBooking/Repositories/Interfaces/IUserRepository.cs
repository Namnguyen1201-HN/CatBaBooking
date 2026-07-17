using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IUserRepository
{
    User? GetByEmail(string email);
    User? GetActiveUserByEmail(string email);
    bool AnyEmail(string email);
    User AddUser(User user);
    void UpdateUser(User user);
}
