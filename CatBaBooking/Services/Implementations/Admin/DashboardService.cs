using System;
using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public (int TotalUsers, int ActiveBusinesses, decimal MonthlyRevenue, int PendingApplications, int TotalBookings, int PendingPayments) GetStats()
    {
        int totalUsers = _dashboardRepository.CountUsers();
        int activeBusinesses = _dashboardRepository.CountActiveBusinesses();
        int pendingApplications = _dashboardRepository.CountPendingBusinesses();
        int totalBookings = _dashboardRepository.CountBookings();
        int pendingPayments = _dashboardRepository.CountUnpaidBookings();

        var revenueRaw = _dashboardRepository.GetMonthlyRevenue(DateTime.Now.Year);
        decimal monthlyRevenue = revenueRaw.FirstOrDefault(r => r.Month == DateTime.Now.Month).Total;

        return (totalUsers, activeBusinesses, monthlyRevenue, pendingApplications, totalBookings, pendingPayments);
    }

    public (List<string> Labels, List<decimal> Data) GetRevenueChart()
    {
        var revenueRaw = _dashboardRepository.GetMonthlyRevenue(DateTime.Now.Year);

        var labels = Enumerable.Range(1, 12).Select(m => $"T{m}").ToList();
        var data = Enumerable.Range(1, 12)
            .Select(m => revenueRaw.FirstOrDefault(r => r.Month == m).Total)
            .ToList();

        return (labels, data);
    }

    public (int Confirmed, int Pending, int Cancelled) GetBookingStatusCounts()
    {
        var statusCounts = _dashboardRepository.GetBookingStatusCounts();

        int confirmed = statusCounts.Where(s => s.Status is "confirmed" or "completed").Sum(s => s.Count);
        int pending = statusCounts.Where(s => s.Status == "pending").Sum(s => s.Count);
        int cancelled = statusCounts.Where(s => s.Status is "cancelled_by_user" or "cancelled_by_owner" or "no_show").Sum(s => s.Count);

        return (confirmed, pending, cancelled);
    }

    public (List<string> Labels, List<int> HomestayData, List<int> RestaurantData) GetTypeChart()
    {
        const int monthsBack = 6;
        var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-(monthsBack - 1));

        var typeRaw = _dashboardRepository.GetBookingCountsByTypeSince(startDate);
        var months = Enumerable.Range(0, monthsBack).Select(i => startDate.AddMonths(i)).ToList();

        var labels = months.Select(d => d.ToString("MM/yy")).ToList();
        var homestayData = months
            .Select(d => typeRaw.Where(t => t.Month == d && t.BusinessType == "homestay").Sum(t => t.Count))
            .ToList();
        var restaurantData = months
            .Select(d => typeRaw.Where(t => t.Month == d && t.BusinessType == "restaurant").Sum(t => t.Count))
            .ToList();

        return (labels, homestayData, restaurantData);
    }

    public List<Booking> GetRecentBookings(int take = 5)
    {
        return _dashboardRepository.GetRecentBookings(take);
    }
}
