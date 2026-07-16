using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class BookedTable
{
    public int BookedTableId { get; set; }

    public int BookingId { get; set; }

    public int TableId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual RestaurantTable Table { get; set; } = null!;
}
