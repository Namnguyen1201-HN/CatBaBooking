using System;
using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Admin;

public interface IDashboardService
{
    (int TotalUsers, int ActiveBusinesses, decimal MonthlyRevenue, int PendingApplications, int TotalBookings, int PendingPayments) GetStats();

    (List<string> Labels, List<decimal> Data) GetRevenueChart();

    (int Confirmed, int Pending, int Cancelled) GetBookingStatusCounts();

    (List<string> Labels, List<int> HomestayData, List<int> RestaurantData) GetTypeChart();

    List<Booking> GetRecentBookings(int take = 5);
}
