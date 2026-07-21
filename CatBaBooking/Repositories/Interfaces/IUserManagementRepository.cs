using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IUserManagementRepository
{
    (List<User> Users, int TotalCount) GetPagedUsers(
        string? searchTerm,
        int? roleId,
        string? status,
        int pageNumber,
        int pageSize);

    User? GetById(int userId);

    bool UpdateStatus(int userId, string newStatus);

    List<Role> GetAssignableRoles();
}
