using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class TableAvailability
{
    public long AvailabilityId { get; set; }

    public int TableId { get; set; }

    public DateOnly ReservationDate { get; set; }

    public TimeOnly ReservationTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual RestaurantTable Table { get; set; } = null!;
}
