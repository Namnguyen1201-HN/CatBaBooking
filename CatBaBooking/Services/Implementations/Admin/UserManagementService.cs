using CatBaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Service.Admin;

public class UserManagementService
{
    private readonly CatbabookingContext con;

    public UserManagementService(CatbabookingContext con)
    {
        this.con = con;
    }

    public (IEnumerable<User> Users, int TotalCount) GetPagedUsers(
        string? searchTerm,
        int? roleId,
        string? status,
        int pageNumber,
        int pageSize)
    {
        var query = con.Users.AsNoTracking().Include(u => u.Role)
            .Where(u => u.RoleId != 3).AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string term = searchTerm.Trim().ToLower();
            query = query.Where(u => u.Email.ToLower().Contains(term) || u.FullName.ToLower().Contains(term));
        }

        if (roleId.HasValue && roleId != 3)
        {
            query = query.Where(u => u.RoleId == roleId.Value);
        }

        if (!string.IsNullOrWhiteSpace(status) && status != "Tất cả")
        {
            query = query.Where(u => u.Status == status);
        }

        query = query.OrderByDescending(u => u.UpdatedAt);
        int totalCount = query.Count();
        var users = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return (users, totalCount);
    }

    // public bool ToggleUserStatus(int userId, string status)
    // {
    //     
    // }
}
