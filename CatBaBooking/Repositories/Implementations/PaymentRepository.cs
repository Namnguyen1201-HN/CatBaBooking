using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class PaymentRepository : IPaymentRepository
{
    private readonly CatbabookingContext _context;

    public PaymentRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<Payment?> GetByIdAsync(int paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByBookingIdAsync(int bookingId)
    {
        // TODO: .FirstOrDefaultAsync(p => p.BookingId == bookingId)
        throw new NotImplementedException();
    }

    public Task<Payment> CreateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int paymentId, string status)
    {
        throw new NotImplementedException();
    }
}
