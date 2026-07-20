using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Admin;

public interface IBookingHistoryService
{
    (List<Booking> Bookings, int TotalCount) GetPagedBookings(
        string? status,
        string? businessType,
        string? searchTerm,
        int pageNumber,
        int pageSize);
}
