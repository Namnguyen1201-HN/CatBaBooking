using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class RoomAvailability
{
    public long AvailabilityId { get; set; }

    public int RoomId { get; set; }

    public DateOnly Date { get; set; }

    public decimal? Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
