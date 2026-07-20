using System;
using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IDashboardRepository
{
    int CountUsers();
    int CountActiveBusinesses();
    int CountPendingBusinesses();
    int CountBookings();
    int CountUnpaidBookings();

    /// <summary>Tổng amount của các payment status = 'completed', theo từng tháng trong năm truyền vào.</summary>
    List<(int Month, decimal Total)> GetMonthlyRevenue(int year);

    /// <summary>Số lượng booking theo từng giá trị status hiện có trong DB.</summary>
    List<(string Status, int Count)> GetBookingStatusCounts();

    /// <summary>Số lượng booking theo tháng + loại hình business, từ mốc thời gian truyền vào tới hiện tại.</summary>
    List<(DateTime Month, string BusinessType, int Count)> GetBookingCountsByTypeSince(DateTime sinceMonth);

    List<Booking> GetRecentBookings(int take);
}
