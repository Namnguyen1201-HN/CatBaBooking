using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class BusinessStatsService : IBusinessStatsService
{
    private static readonly string[] ValidSorts =
    {
        "booking_desc", "booking_asc", "revenue_desc", "revenue_asc", "rating_desc", "rating_asc"
    };

    private readonly IBusinessStatsRepository _businessStatsRepository;

    public BusinessStatsService(IBusinessStatsRepository businessStatsRepository)
    {
        _businessStatsRepository = businessStatsRepository;
    }

    public (List<(Business Business, int BookingCount, decimal Revenue, double AvgRating, int ReviewCount)> Rows, int TotalCount)
        GetBusinessStats(string? businessType, string? sortBy, int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        string? normalizedType = string.IsNullOrWhiteSpace(businessType) || businessType == "all" ? null : businessType;
        string? normalizedSort = ValidSorts.Contains(sortBy) ? sortBy : "booking_desc";

        return _businessStatsRepository.GetBusinessStats(normalizedType, normalizedSort, pageNumber, pageSize);
    }
}
