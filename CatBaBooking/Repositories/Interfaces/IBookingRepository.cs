using CatBaBooking.Models;
using System.Collections.Generic;

namespace CatBaBooking.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Booking? CreateBooking(Booking booking, List<BookingDish> dishes);
        Booking? GetBookingByCode(string bookingCode);
    }
}
