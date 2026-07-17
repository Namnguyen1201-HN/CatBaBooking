using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repositories.Implementations;

/// <summary>
/// Triển khai IUserRepository dùng EF Core + CatbabookingContext.
/// 
/// [HƯỚNG DẪN] Các bước implement một method:
///   1. Nhận _context từ constructor (đã inject sẵn)
///   2. Dùng LINQ / EF Core để truy vấn DB
///   3. Trả về kết quả (entity hoặc null)
///   Ví dụ: return await _context.Users.FindAsync(userId);
/// </summary>
public class UserRepository : IUserRepository
{
    // _context là "cổng vào" database — được inject qua DI (xem Program.cs)
    private readonly CatbabookingContext _context;

    public UserRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        // TODO: return await _context.Users.FindAsync(userId);
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        // TODO: return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        // TODO: return (IEnumerable<User>) await _context.Users.Include(u => u.Role).ToListAsync();
        throw new NotImplementedException();
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        // TODO: return await _context.Users.AnyAsync(u => u.Email == email);
        throw new NotImplementedException();
    }

    public Task<User> CreateAsync(User user)
    {
        // TODO: _context.Users.Add(user); await _context.SaveChangesAsync(); return user;
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        // TODO: _context.Users.Update(user); await _context.SaveChangesAsync();
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int userId, string status)
    {
        // TODO: Tìm user theo id, đổi Status, rồi SaveChanges
        throw new NotImplementedException();
    }

    public Task UpdatePasswordAsync(int userId, string hashedPassword)
    {
        // TODO: Tìm user, đổi Password = hashedPassword, SaveChanges
        throw new NotImplementedException();
    }
}
