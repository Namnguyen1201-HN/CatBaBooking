using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class DishCategory
{
    public int CategoryId { get; set; }

    public int BusinessId { get; set; }

    public string Name { get; set; } = null!;

    public int? DisplayOrder { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
