using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Dish
{
    public int DishId { get; set; }

    public int BusinessId { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<BookingDish> BookingDishes { get; set; } = new List<BookingDish>();

    public virtual Business Business { get; set; } = null!;

    public virtual DishCategory? Category { get; set; }

    public virtual ICollection<TempCart> TempCarts { get; set; } = new List<TempCart>();
}
