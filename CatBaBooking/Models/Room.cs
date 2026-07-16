using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int BusinessId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal PricePerNight { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<BookedRoom> BookedRooms { get; set; } = new List<BookedRoom>();

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<RoomAvailability> RoomAvailabilities { get; set; } = new List<RoomAvailability>();

    public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
}
