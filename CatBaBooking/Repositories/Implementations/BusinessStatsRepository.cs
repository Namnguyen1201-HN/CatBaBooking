using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class BusinessStatsRepository : IBusinessStatsRepository
{
    private readonly CatbabookingContext _context;

    public BusinessStatsRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public (List<(Business Business, int BookingCount, decimal Revenue, double AvgRating, int ReviewCount)> Rows, int TotalCount)
        GetBusinessStats(string? businessType, string? sortBy, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Businesses.AsQueryable();

        if (!string.IsNullOrEmpty(businessType))
        {
            baseQuery = baseQuery.Where(b => b.Type == businessType);
        }

        // Mỗi business được đính kèm 3 subquery tương quan: số booking, tổng doanh thu (payment completed),
        // và điểm đánh giá trung bình + số lượt đánh giá.
        var statsQuery = baseQuery.Select(b => new
        {
            Business = b,
            BookingCount = _context.Bookings.Count(bk => bk.BusinessId == b.BusinessId),
            Revenue = _context.Payments
                .Where(p => p.Status == "completed" && p.Booking.BusinessId == b.BusinessId)
                .Sum(p => (decimal?)p.Amount) ?? 0m,
            AvgRating = _context.Reviews
                .Where(r => r.BusinessId == b.BusinessId)
                .Average(r => (double?)r.Rating) ?? 0,
            ReviewCount = _context.Reviews.Count(r => r.BusinessId == b.BusinessId)
        });

        statsQuery = sortBy switch
        {
            "booking_asc" => statsQuery.OrderBy(x => x.BookingCount),
            "revenue_desc" => statsQuery.OrderByDescending(x => x.Revenue),
            "revenue_asc" => statsQuery.OrderBy(x => x.Revenue),
            "rating_desc" => statsQuery.OrderByDescending(x => x.AvgRating),
            "rating_asc" => statsQuery.OrderBy(x => x.AvgRating),
            _ => statsQuery.OrderByDescending(x => x.BookingCount) // mặc định: nhiều booking nhất trước
        };

        int totalCount = statsQuery.Count();

        var page = statsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var rows = page
            .Select(x => (x.Business, x.BookingCount, x.Revenue, x.AvgRating, x.ReviewCount))
            .ToList();

        return (rows, totalCount);
    }
}
