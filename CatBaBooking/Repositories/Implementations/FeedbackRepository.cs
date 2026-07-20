using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly CatbabookingContext _context;

    public FeedbackRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public (List<Review> Reviews, int TotalCount) GetPagedReviews(
        string? searchTerm,
        int? rating,
        string? sortBy,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Business)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string term = searchTerm.Trim().ToLower();
            query = query.Where(r =>
                r.User.FullName.ToLower().Contains(term) ||
                r.Business.Name.ToLower().Contains(term) ||
                (r.Comment != null && r.Comment.ToLower().Contains(term)));
        }

        if (rating.HasValue)
        {
            query = query.Where(r => r.Rating == (byte)rating.Value);
        }

        query = sortBy switch
        {
            "rating_desc" => query.OrderByDescending(r => r.Rating).ThenByDescending(r => r.CreatedAt),
            "rating_asc" => query.OrderBy(r => r.Rating).ThenByDescending(r => r.CreatedAt),
            _ => query.OrderByDescending(r => r.CreatedAt)
        };

        int totalCount = query.Count();
        var reviews = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (reviews, totalCount);
    }

    public Review? GetById(int reviewId)
    {
        return _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Business)
            .Include(r => r.Booking)
            .FirstOrDefault(r => r.ReviewId == reviewId);
    }

    public bool DeleteReview(int reviewId)
    {
        var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        if (review == null) return false;

        _context.Reviews.Remove(review);
        _context.SaveChanges();
        return true;
    }
}
