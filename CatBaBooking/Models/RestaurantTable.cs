using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class RestaurantTable
{
    public int TableId { get; set; }

    public int BusinessId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<BookedTable> BookedTables { get; set; } = new List<BookedTable>();

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<TableAvailability> TableAvailabilities { get; set; } = new List<TableAvailability>();
}
