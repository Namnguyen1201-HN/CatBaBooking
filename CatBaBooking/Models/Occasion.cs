using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Occasion
{
    public int OccasionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
