using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;

namespace CatBaBooking.Service;

public class OwnerDashboardService : IOwnerDashboardService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IBookingRepository _bookingRepository;

    public OwnerDashboardService(IBusinessRepository businessRepository, IBookingRepository bookingRepository)
    {
        _businessRepository = businessRepository;
        _bookingRepository = bookingRepository;
    }

    public OwnerDashboardViewModel GetDashboardData(int ownerId)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);

        if (business == null || business.Type != "restaurant")
        {
            return new OwnerDashboardViewModel { HasRestaurant = false };
        }

        var bookings = _bookingRepository.GetByBusinessId(business.BusinessId);

        var totalRevenue = bookings
            .Where(b => b.PaymentStatus == "fully_paid")
            .Sum(b => (decimal?)b.TotalPrice) ?? 0;

        var totalOrders = bookings.Count;
        var pendingOrders = bookings.Count(b => b.Status == "pending");

        var recentBookings = bookings
            .OrderByDescending(b => b.CreatedAt)
            .Take(5)
            .Select(b => new RecentBookingViewModel
            {
                BookingId = b.BookingId,
                BookingCode = b.BookingCode,
                BookerName = b.BookerName,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            })
            .ToList();

        foreach (var item in recentBookings)
        {
            (item.StatusLabel, item.StatusBadgeClass) = GetStatusDisplay(item.Status);
        }

        return new OwnerDashboardViewModel
        {
            HasRestaurant = true,
            RestaurantName = business.Name,
            TotalRevenue = totalRevenue,
            TotalOrders = totalOrders,
            PendingOrders = pendingOrders,
            AvgRating = business.AvgRating ?? 0,
            ReviewCount = business.ReviewCount ?? 0,
            RecentBookings = recentBookings
        };
    }

    private static (string Label, string CssClass) GetStatusDisplay(string status)
    {
        return status switch
        {
            "confirmed" => ("Đã xác nhận", "status-confirmed"),
            "completed" => ("Hoàn thành", "status-completed"),
            "pending" => ("Chờ xác nhận", "status-pending"),
            "cancelled_by_user" => ("Đã hủy", "status-cancelled"),
            "cancelled_by_owner" => ("Đã hủy", "status-cancelled"),
            "no_show" => ("Không đến", "status-noshow"),
            _ => (status, "status-default")
        };
    }
}