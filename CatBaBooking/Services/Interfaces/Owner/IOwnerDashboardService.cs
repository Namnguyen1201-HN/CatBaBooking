using CatBaBooking.ViewModels;

namespace CatBaBooking.Service.Interface;

public interface IOwnerDashboardService
{
    OwnerDashboardViewModel GetDashboardData(int ownerId);
}