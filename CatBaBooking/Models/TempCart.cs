using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class TempCart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int BusinessId { get; set; }

    public int DishId { get; set; }

    public int Quantity { get; set; }

    public string? Notes { get; set; }

    public decimal Subtotal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual Dish Dish { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
