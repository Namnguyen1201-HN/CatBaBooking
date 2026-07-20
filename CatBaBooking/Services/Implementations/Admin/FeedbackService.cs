using System.Collections.Generic;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public (List<Review> Reviews, int TotalCount) GetPagedReviews(
        string? searchTerm,
        int? rating,
        string? sortBy,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        string? normalizedSearch = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();
        int? normalizedRating = rating is >= 1 and <= 5 ? rating : null;

        return _feedbackRepository.GetPagedReviews(normalizedSearch, normalizedRating, sortBy, pageNumber, pageSize);
    }

    public Review? GetReviewDetail(int reviewId)
    {
        return _feedbackRepository.GetById(reviewId);
    }

    public bool DeleteReview(int reviewId)
    {
        return _feedbackRepository.DeleteReview(reviewId);
    }
}
