using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? BookingId { get; set; }

    public int BusinessId { get; set; }

    public int UserId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
