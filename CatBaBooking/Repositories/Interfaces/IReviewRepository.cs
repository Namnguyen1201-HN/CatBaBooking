using CatBaBooking.Models;

namespace CatBaBooking.Repositories.Interfaces;

/// <summary>
/// Định nghĩa các thao tác truy cập dữ liệu cho Review (đánh giá).
/// </summary>
public interface IReviewRepository
{
    /// <summary>Lấy danh sách review theo Business.</summary>
    Task<IEnumerable<Review>> GetByBusinessIdAsync(int businessId);

    /// <summary>Lấy review của một user cho một business cụ thể.</summary>
    Task<Review?> GetByUserAndBusinessAsync(int userId, int businessId);

    Task<Review> CreateAsync(Review review);
    Task DeleteAsync(int reviewId);
}
