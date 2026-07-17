namespace CatBaBooking.ViewModels.Payment;

/// <summary>Kết quả sau khi thanh toán xong.</summary>
public class PaymentResultViewModel
{
    public int BookingId { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime? PaidAt { get; set; }
}
