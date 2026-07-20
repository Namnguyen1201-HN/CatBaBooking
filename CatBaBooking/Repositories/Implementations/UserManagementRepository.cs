using System;
using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class UserManagementRepository : IUserManagementRepository
{
    private readonly CatbabookingContext _context;

    public UserManagementRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public (List<User> Users, int TotalCount) GetPagedUsers(
        string? searchTerm,
        int? roleId,
        string? status,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Users
            .Include(u => u.Role)
            .Where(u => u.RoleId != 3) // không quản lý tài khoản admin ở đây
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string term = searchTerm.Trim().ToLower();
            query = query.Where(u => u.Email.ToLower().Contains(term) || u.FullName.ToLower().Contains(term));
        }

        if (roleId.HasValue)
        {
            query = query.Where(u => u.RoleId == roleId.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(u => u.Status == status);
        }

        query = query.OrderByDescending(u => u.UpdatedAt);

        int totalCount = query.Count();
        var users = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (users, totalCount);
    }

    public User? GetById(int userId)
    {
        return _context.Users
            .Include(u => u.Role)
            .Include(u => u.Businesses)
            .FirstOrDefault(u => u.UserId == userId);
    }

    public bool UpdateStatus(int userId, string newStatus)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null) return false;

        user.Status = newStatus;
        user.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return true;
    }

    public List<Role> GetAssignableRoles()
    {
        return _context.Roles.Where(r => r.RoleId != 3).ToList();
    }
}
