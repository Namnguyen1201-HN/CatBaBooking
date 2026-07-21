using System;

namespace CatBaBooking.ViewModels.Payment
{
    public class ConfirmationPaymentViewModel
    {
        public string BookingCode { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public DateOnly? ReservationDate { get; set; }
        public TimeOnly? ReservationTime { get; set; }
        public int NumGuests { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
