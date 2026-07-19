using System;

namespace CatBaBooking.ViewModels.Homestay
{
    public class BookingHomestayViewModel
    {
        public int RoomId { get; set; }
        public int BusinessId { get; set; }
        public string? RoomName { get; set; }
        public string? HomestayName { get; set; }
        public string? Address { get; set; }
        public decimal PricePerNight { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int MaxCapacity { get; set; }
    }
}
