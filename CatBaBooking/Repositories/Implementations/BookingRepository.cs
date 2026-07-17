using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class BookingRepository : IBookingRepository
{
    private readonly CatbabookingContext _context;

    public BookingRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<Booking?> GetByIdAsync(int bookingId)
    {
        // TODO: .Include(b => b.BookedRooms).Include(b => b.BookedTables)
        //        .Include(b => b.BookingDishes).FirstOrDefaultAsync(b => b.BookingId == bookingId)
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
    {
        // TODO: .Where(b => b.UserId == userId).OrderByDescending(b => b.CreatedAt).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Booking>> GetByBusinessIdAsync(int businessId)
    {
        // TODO: Join với BookedRooms/BookedTables để lọc theo businessId
        throw new NotImplementedException();
    }

    public Task<Booking> CreateAsync(Booking booking)
    {
        // TODO: _context.Bookings.Add(booking); SaveChanges; return booking;
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int bookingId, string status)
    {
        // TODO: Tìm booking, đổi Status, SaveChanges
        throw new NotImplementedException();
    }

    public Task CancelAsync(int bookingId)
    {
        // TODO: Gọi UpdateStatusAsync(bookingId, "cancelled")
        throw new NotImplementedException();
    }
}
