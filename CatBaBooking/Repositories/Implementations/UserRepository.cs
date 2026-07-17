using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class UserRepository : IUserRepository
{
    private readonly CatbabookingContext _context;

    public UserRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == email);
    }

    public User? GetActiveUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(x => x.Email == email && x.Status == "active");
    }

    public bool AnyEmail(string email)
    {
        return _context.Users.Any(x => x.Email == email);
    }

    public User AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    // public List<User> GetAllUser(User user)
    // {
    //     
    //     return _context.Users.AsNoTracking()
    //         .Include(x => x.Role)
    //         .Where(u => u.RoleId != 3).AsQueryable();
    // }
}
