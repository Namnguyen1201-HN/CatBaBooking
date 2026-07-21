using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class BookingHistoryRepository : IBookingHistoryRepository
{
    private readonly CatbabookingContext _context;

    public BookingHistoryRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public (List<Booking> Bookings, int TotalCount) GetPagedBookings(
        string? status,
        string? businessType,
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Bookings
            .Include(b => b.Business)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(b => b.Status == status);
        }

        if (!string.IsNullOrEmpty(businessType))
        {
            query = query.Where(b => b.Business.Type == businessType);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(b =>
                b.BookingCode.Contains(searchTerm) ||
                b.BookerName.Contains(searchTerm) ||
                b.BookerEmail.Contains(searchTerm) ||
                b.BookerPhone.Contains(searchTerm));
        }

        query = query.OrderByDescending(b => b.CreatedAt);

        int totalCount = query.Count();
        var bookings = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (bookings, totalCount);
    }
}
