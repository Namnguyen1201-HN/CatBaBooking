using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Admin;

public interface IFeedbackService
{
    (List<Review> Reviews, int TotalCount) GetPagedReviews(
        string? searchTerm,
        int? rating,
        string? sortBy,
        int pageNumber,
        int pageSize);

    Review? GetReviewDetail(int reviewId);

    bool DeleteReview(int reviewId);
}
