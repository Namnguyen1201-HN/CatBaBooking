using CatBaBooking.ViewModels.Review;

namespace CatBaBooking.Services.Interfaces;

/// <summary>
/// Tầng Service xử lý business logic cho Review (đánh giá dịch vụ).
/// </summary>
public interface IReviewService
{
    /// <summary>Lấy danh sách reviews cho một business.</summary>
    Task<IEnumerable<ReviewViewModel>> GetReviewsAsync(int businessId);

    /// <summary>
    /// Gửi đánh giá mới.
    /// [Service sẽ: kiểm tra user đã booking business này chưa → kiểm tra đã review chưa → tạo review]
    /// </summary>
    Task<(bool Success, string ErrorMessage)> LeaveReviewAsync(LeaveReviewViewModel model, int userId);

    /// <summary>Xóa review (Admin hoặc chính user đó).</summary>
    Task<bool> DeleteReviewAsync(int reviewId, int requesterId);
}
