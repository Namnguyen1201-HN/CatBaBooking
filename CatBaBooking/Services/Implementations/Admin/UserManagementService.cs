using System.Collections.Generic;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class UserManagementService : IUserManagementService
{
    private readonly IUserManagementRepository _userManagementRepository;

    public UserManagementService(IUserManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    public (IEnumerable<User> Users, int TotalCount) GetPagedUsers(
        string? searchTerm,
        int? roleId,
        string? status,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        string? normalizedStatus = string.IsNullOrWhiteSpace(status) || status == "all" ? null : status;

        var (users, totalCount) = _userManagementRepository.GetPagedUsers(searchTerm, roleId, normalizedStatus, pageNumber, pageSize);
        return (users, totalCount);
    }

    public User? GetUserById(int userId)
    {
        return _userManagementRepository.GetById(userId);
    }

    public bool ToggleUserStatus(int userId, string newStatus)
    {
        if (newStatus != "active" && newStatus != "rejected" && newStatus != "pending")
        {
            return false;
        }

        return _userManagementRepository.UpdateStatus(userId, newStatus);
    }

    public List<Role> GetAssignableRoles()
    {
        return _userManagementRepository.GetAssignableRoles();
    }
}
