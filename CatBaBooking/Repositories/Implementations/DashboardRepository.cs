using System;
using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly CatbabookingContext _context;

    public DashboardRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public int CountUsers()
    {
        return _context.Users.Count();
    }

    public int CountActiveBusinesses()
    {
        return _context.Businesses.Count(b => b.Status == "active");
    }

    public int CountPendingBusinesses()
    {
        return _context.Businesses.Count(b => b.Status == "pending");
    }

    public int CountBookings()
    {
        return _context.Bookings.Count();
    }

    public int CountUnpaidBookings()
    {
        return _context.Bookings.Count(b => b.PaymentStatus == "unpaid");
    }

    public List<(int Month, decimal Total)> GetMonthlyRevenue(int year)
    {
        return _context.Payments
            .Where(p => p.Status == "completed" && p.CreatedAt != null && p.CreatedAt.Value.Year == year)
            .GroupBy(p => p.CreatedAt!.Value.Month)
            .Select(g => new { Month = g.Key, Total = g.Sum(x => x.Amount) })
            .ToList()
            .Select(x => (x.Month, x.Total))
            .ToList();
    }

    public List<(string Status, int Count)> GetBookingStatusCounts()
    {
        return _context.Bookings
            .GroupBy(b => b.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToList()
            .Select(x => (x.Status, x.Count))
            .ToList();
    }

    public List<(DateTime Month, string BusinessType, int Count)> GetBookingCountsByTypeSince(DateTime sinceMonth)
    {
        // Lấy dữ liệu thô về rồi group trong memory vì cần gom theo (năm, tháng, type) của navigation property Business.Type
        var raw = _context.Bookings
            .Include(b => b.Business)
            .Where(b => b.CreatedAt != null && b.CreatedAt >= sinceMonth)
            .Select(b => new { b.CreatedAt, BusinessType = b.Business.Type })
            .ToList();

        return raw
            .GroupBy(x => new
            {
                Month = new DateTime(x.CreatedAt!.Value.Year, x.CreatedAt.Value.Month, 1),
                x.BusinessType
            })
            .Select(g => (g.Key.Month, g.Key.BusinessType, g.Count()))
            .ToList();
    }

    public List<Booking> GetRecentBookings(int take)
    {
        return _context.Bookings
            .Include(b => b.Business)
            .OrderByDescending(b => b.CreatedAt)
            .Take(take)
            .ToList();
    }
}
