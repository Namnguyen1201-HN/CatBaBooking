using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class RestaurantType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
