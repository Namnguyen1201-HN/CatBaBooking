using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CatbabookingContext _context;

        public BookingRepository(CatbabookingContext context)
        {
            _context = context;
        }

        public Booking? CreateBooking(Booking booking, List<BookingDish> dishes)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();

                if (dishes != null && dishes.Any())
                {
                    foreach (var dish in dishes)
                    {
                        dish.BookingId = booking.BookingId; // Assign the generated booking ID
                        _context.BookingDishes.Add(dish);
                    }
                    _context.SaveChanges();
                }

                transaction.Commit();
                return booking;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
        }

        public Booking? GetBookingByCode(string bookingCode)
        {
            return _context.Bookings
                .Include(b => b.Business)
                .FirstOrDefault(b => b.BookingCode == bookingCode);
        }
    }
}
