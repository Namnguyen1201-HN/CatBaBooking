using System.Collections.Generic;

namespace CatBaBooking.ViewModels
{
    // ViewModel tổng hợp dữ liệu hiển thị trên trang OwnerDashboard
    public class OwnerDashboardViewModel
    {
        public bool HasRestaurant { get; set; } = false;
        public string RestaurantName { get; set; } = string.Empty;

        public decimal TotalRevenue { get; set; } = 0;
        public int TotalOrders { get; set; } = 0;
        public int PendingOrders { get; set; } = 0;
        public decimal AvgRating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        public List<RecentBookingViewModel> RecentBookings { get; set; } = new();
    }

    public class RecentBookingViewModel
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string BookerName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public string StatusLabel { get; set; } = string.Empty;
        public string StatusBadgeClass { get; set; } = string.Empty;
    }
}