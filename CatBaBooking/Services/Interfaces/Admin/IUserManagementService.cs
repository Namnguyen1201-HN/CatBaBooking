using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Admin;

public interface IUserManagementService
{
    (IEnumerable<User> Users, int TotalCount) GetPagedUsers(
        string searchTerm,
        int roleId,
        string status,
        int pageNumber,
        int pageSize);
    User GetUserById(int userId);
    bool ToggleUserStatus(int userId, string status);
}
