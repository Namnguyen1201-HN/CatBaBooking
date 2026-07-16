using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class BookedRoom
{
    public int BookedRoomId { get; set; }

    public int BookingId { get; set; }

    public int RoomId { get; set; }

    public decimal PriceAtBooking { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
