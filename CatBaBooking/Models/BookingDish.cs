using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class BookingDish
{
    public int BookingDishId { get; set; }

    public int BookingId { get; set; }

    public int? DishId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceAtBooking { get; set; }

    public string? Notes { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Dish? Dish { get; set; }
}
