using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IFeedbackRepository
{
    (List<Review> Reviews, int TotalCount) GetPagedReviews(
        string? searchTerm,
        int? rating,
        string? sortBy,
        int pageNumber,
        int pageSize);

    Review? GetById(int reviewId);

    bool DeleteReview(int reviewId);
}
