using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Feature
{
    public int FeatureId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
