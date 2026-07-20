using System.Collections.Generic;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class BookingHistoryService : IBookingHistoryService
{
    private readonly IBookingHistoryRepository _bookingHistoryRepository;

    public BookingHistoryService(IBookingHistoryRepository bookingHistoryRepository)
    {
        _bookingHistoryRepository = bookingHistoryRepository;
    }

    public (List<Booking> Bookings, int TotalCount) GetPagedBookings(
        string? status,
        string? businessType,
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        string? normalizedStatus = string.IsNullOrWhiteSpace(status) || status == "all" ? null : status;
        string? normalizedType = string.IsNullOrWhiteSpace(businessType) || businessType == "all" ? null : businessType;
        string? normalizedSearch = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();

        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        return _bookingHistoryRepository.GetPagedBookings(normalizedStatus, normalizedType, normalizedSearch, pageNumber, pageSize);
    }
}
