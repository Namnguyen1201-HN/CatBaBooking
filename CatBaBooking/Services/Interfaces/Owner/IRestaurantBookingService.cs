using CatBaBooking.ViewModels;

namespace CatBaBooking.Service.Interface;

public interface IRestaurantBookingService
{
    RestaurantBookingsViewModel GetBookings(
        int ownerId,
        DateOnly? reservationDate,
        TimeOnly? reservationTime,
        int? numGuests,
        string? status,
        string? searchCode,
        int page);
}