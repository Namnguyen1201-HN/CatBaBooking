using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Review;

namespace CatBaBooking.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookingRepository _bookingRepository;

    public ReviewService(IReviewRepository reviewRepository, IBookingRepository bookingRepository)
    {
        _reviewRepository = reviewRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<ReviewViewModel>> GetReviewsAsync(int businessId)
    {
        // TODO: _reviewRepository.GetByBusinessIdAsync(businessId) → map → list ReviewViewModel
        throw new NotImplementedException();
    }

    public async Task<(bool Success, string ErrorMessage)> LeaveReviewAsync(LeaveReviewViewModel model, int userId)
    {
        // TODO:
        // 1. Kiểm tra user đã có booking "completed" tại business này chưa
        //    (Chỉ cho review nếu đã dùng dịch vụ)
        // 2. Kiểm tra đã review rồi: _reviewRepository.GetByUserAndBusinessAsync(userId, model.BusinessId)
        // 3. Tạo Review entity → _reviewRepository.CreateAsync(review)
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteReviewAsync(int reviewId, int requesterId)
    {
        // TODO: Kiểm tra review.UserId == requesterId HOẶC requesterId là Admin → xóa
        throw new NotImplementedException();
    }
}
