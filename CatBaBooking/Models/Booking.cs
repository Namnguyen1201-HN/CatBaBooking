using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingCode { get; set; } = null!;

    public int? UserId { get; set; }

    public int BusinessId { get; set; }

    public string BookerName { get; set; } = null!;

    public string BookerEmail { get; set; } = null!;

    public string BookerPhone { get; set; } = null!;

    public int NumGuests { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal? PaidAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Notes { get; set; }

    public DateTime? ReservationStartTime { get; set; }

    public DateTime? ReservationEndTime { get; set; }

    public string Status { get; set; } = null!;

    public TimeOnly? ReservationTime { get; set; }

    public DateOnly? ReservationDate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BookedRoom> BookedRooms { get; set; } = new List<BookedRoom>();

    public virtual ICollection<BookedTable> BookedTables { get; set; } = new List<BookedTable>();

    public virtual ICollection<BookingDish> BookingDishes { get; set; } = new List<BookingDish>();

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Review? Review { get; set; }

    public virtual User? User { get; set; }
}
