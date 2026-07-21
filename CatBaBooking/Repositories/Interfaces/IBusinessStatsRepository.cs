using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IBusinessStatsRepository
{
    (List<(Business Business, int BookingCount, decimal Revenue, double AvgRating, int ReviewCount)> Rows, int TotalCount)
        GetBusinessStats(string? businessType, string? sortBy, int pageNumber, int pageSize);
}
