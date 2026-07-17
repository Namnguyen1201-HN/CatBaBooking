using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Auth;

namespace CatBaBooking.Services.Implementations;

/// <summary>
/// Triển khai IUserService.
/// 
/// [HƯỚNG DẪN] Service nhận vào các INTERFACE của Repository (không phải class cụ thể).
/// Lý do: dễ test, dễ swap implementation sau này.
/// </summary>
public class UserService : IUserService
{
    // Service phụ thuộc vào Repository và Helpers — inject qua constructor
    private readonly IUserRepository _userRepository;
    // private readonly IPasswordHelper _passwordHelper; // Uncomment khi có PasswordHelper

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        // TODO:
        // 1. Gọi _userRepository.GetByEmailAsync(email)
        // 2. Nếu user == null → return null
        // 3. Dùng PasswordHelper.Verify(password, user.Password) → nếu sai → return null
        // 4. Kiểm tra user.Status == "active" → nếu bị ban → return null
        // 5. return user
        throw new NotImplementedException();
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
    {
        // TODO:
        // 1. Kiểm tra email đã tồn tại: _userRepository.EmailExistsAsync(model.Email)
        // 2. Nếu tồn tại → return (false, "Email đã được sử dụng")
        // 3. Hash password: PasswordHelper.Hash(model.Password)
        // 4. Tạo User mới với RoleId = [Customer role id]
        // 5. _userRepository.CreateAsync(newUser)
        // 6. return (true, "")
        throw new NotImplementedException();
    }

    public async Task<User?> GetProfileAsync(int userId)
    {
        // TODO: return await _userRepository.GetByIdAsync(userId);
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateProfileAsync(int userId, string fullName, string phone, string? avatarUrl)
    {
        // TODO: Lấy user → cập nhật fields → gọi _userRepository.UpdateAsync(user)
        throw new NotImplementedException();
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        // TODO:
        // 1. Lấy user theo id
        // 2. Verify oldPassword với user.Password (hash)
        // 3. Nếu sai → return false
        // 4. Hash newPassword → UpdatePasswordAsync
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // TODO: return await _userRepository.GetAllAsync();
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserStatusAsync(int userId, string status)
    {
        // TODO: _userRepository.UpdateStatusAsync(userId, status); return true;
        throw new NotImplementedException();
    }
}
