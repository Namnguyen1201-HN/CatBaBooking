using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repositories.Implementations
{
namespace CatBaBooking.Repository;

    public class BookingRepository : IBookingRepository
    {
        private readonly CatbabookingContext _context;

        public BookingRepository(CatbabookingContext context)
        {
            _context = context;
        }

        public Booking? CreateBooking(Booking booking, List<BookingDish> dishes)
    public List<Booking> GetByBusinessId(int businessId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
        return _context.Bookings
            .Where(b => b.BusinessId == businessId)
            .ToList();
    }

    public List<BookingDetailDto> GetDetailedByBusinessId(int businessId)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
        var bookings = _context.Bookings
            .Where(b => b.BusinessId == businessId)
            .OrderByDescending(b => b.CreatedAt)
            .ToList();

                if (dishes != null && dishes.Any())
        var bookingIds = bookings.Select(b => b.BookingId).ToList();

        var tableNamesByBooking = _context.BookedTables
            .Where(bt => bookingIds.Contains(bt.BookingId))
            .Join(_context.RestaurantTables, bt => bt.TableId, t => t.TableId,
                (bt, t) => new { bt.BookingId, t.Name })
            .ToList()
            .GroupBy(x => x.BookingId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Name).ToList());

        var dishesByBooking = _context.BookingDishes
            .Where(bd => bookingIds.Contains(bd.BookingId))
            .GroupJoin(_context.Dishes, bd => bd.DishId, d => d.DishId, (bd, dGroup) => new { bd, dGroup })
            .SelectMany(x => x.dGroup.DefaultIfEmpty(), (x, d) => new
                {
                    foreach (var dish in dishes)
                x.bd.BookingId,
                DishName = d != null ? d.Name : "(Món đã xoá)",
                x.bd.Quantity,
                x.bd.PriceAtBooking,
                x.bd.Notes
            })
            .ToList()
            .GroupBy(x => x.BookingId)
            .ToDictionary(g => g.Key, g => g.Select(x => new BookingDishDto
                    {
                        dish.BookingId = booking.BookingId; // Assign the generated booking ID
                        _context.BookingDishes.Add(dish);
                    }
                    _context.SaveChanges();
                }
                DishName = x.DishName,
                Quantity = x.Quantity,
                PriceAtBooking = x.PriceAtBooking,
                Notes = x.Notes
            }).ToList());

                transaction.Commit();
                return booking;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return null;
            }
        }
        var latestPaymentByBooking = _context.Payments
            .Where(p => bookingIds.Contains(p.BookingId))
            .ToList()
            .GroupBy(p => p.BookingId)
            .ToDictionary(g => g.Key, g => g.OrderByDescending(p => p.CreatedAt).FirstOrDefault());

        public Booking? GetBookingByCode(string bookingCode)
        return bookings.Select(b => new BookingDetailDto
        {
            return _context.Bookings
                .Include(b => b.Business)
                .FirstOrDefault(b => b.BookingCode == bookingCode);
        }
            BookingId = b.BookingId,
            BookingCode = b.BookingCode,
            BookerName = b.BookerName,
            BookerEmail = b.BookerEmail,
            BookerPhone = b.BookerPhone,
            NumGuests = b.NumGuests,
            TotalPrice = b.TotalPrice,
            PaymentStatus = b.PaymentStatus,
            Notes = b.Notes,
            Status = b.Status,
            ReservationTime = b.ReservationTime,
            ReservationDate = b.ReservationDate,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt,
            TableNames = tableNamesByBooking.TryGetValue(b.BookingId, out var names) ? names : new List<string>(),
            Dishes = dishesByBooking.TryGetValue(b.BookingId, out var dishes) ? dishes : new List<BookingDishDto>(),
            LastPaymentMethod = latestPaymentByBooking.TryGetValue(b.BookingId, out var pay) ? pay?.PaymentMethod : null
        }).ToList();
    }
}
