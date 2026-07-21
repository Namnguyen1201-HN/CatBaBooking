using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;

namespace CatBaBooking.Service;

public class RestaurantBookingService : IRestaurantBookingService
{
    private const int PageSize = 10;

    private readonly IBusinessRepository _businessRepository;
    private readonly IBookingRepository _bookingRepository;

    public RestaurantBookingService(IBusinessRepository businessRepository, IBookingRepository bookingRepository)
    {
        _businessRepository = businessRepository;
        _bookingRepository = bookingRepository;
    }

    public RestaurantBookingsViewModel GetBookings(
        int ownerId,
        DateOnly? reservationDate,
        TimeOnly? reservationTime,
        int? numGuests,
        string? status,
        string? searchCode,
        int page)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);

        var result = new RestaurantBookingsViewModel
        {
            ReservationDate = reservationDate,
            ReservationTime = reservationTime,
            NumGuests = numGuests,
            Status = status,
            SearchCode = searchCode,
            CurrentPage = page < 1 ? 1 : page
        };

        if (business == null || business.Type != "restaurant")
        {
            return result;
        }

        result.RestaurantName = business.Name;

        var all = _bookingRepository.GetDetailedByBusinessId(business.BusinessId).AsEnumerable();

        if (reservationDate.HasValue)
        {
            all = all.Where(b => b.ReservationDate == reservationDate.Value);
        }

        if (reservationTime.HasValue)
        {
            all = all.Where(b => b.ReservationTime == reservationTime.Value);
        }

        if (numGuests.HasValue)
        {
            all = all.Where(b => b.NumGuests == numGuests.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            all = all.Where(b => b.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(searchCode))
        {
            all = all.Where(b => b.BookingCode.Contains(searchCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        var filtered = all.ToList();

        result.TotalCount = filtered.Count;
        result.TotalPages = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)PageSize));
        result.CurrentPage = Math.Min(result.CurrentPage, result.TotalPages);

        var pageItems = filtered
            .Skip((result.CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .Select(b => new BookingListItemViewModel
            {
                BookingId = b.BookingId,
                BookingCode = b.BookingCode,
                BookerName = b.BookerName,
                BookerEmail = b.BookerEmail,
                BookerPhone = b.BookerPhone,
                TableNames = b.TableNames.Count > 0 ? string.Join(", ", b.TableNames) : "-",
                ReservationDate = b.ReservationDate,
                ReservationTime = b.ReservationTime,
                NumGuests = b.NumGuests,
                TotalPrice = b.TotalPrice,
                Status = b.Status,
                Notes = b.Notes,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                PaymentMethod = b.LastPaymentMethod,
                Dishes = b.Dishes.Select(d => new BookingDishItemViewModel
                {
                    DishName = d.DishName,
                    Quantity = d.Quantity,
                    PriceAtBooking = d.PriceAtBooking,
                    Notes = d.Notes
                }).ToList()
            })
            .ToList();

        foreach (var item in pageItems)
        {
            (item.StatusLabel, item.StatusBadgeClass) = GetStatusDisplay(item.Status);
            item.PaymentStatusLabel = GetPaymentStatusLabel(
                filtered.First(f => f.BookingId == item.BookingId).PaymentStatus);
        }

        result.Bookings = pageItems;
        return result;
    }

    private static (string Label, string CssClass) GetStatusDisplay(string status)
    {
        return status switch
        {
            "confirmed" => ("Đã xác nhận", "bg-success-subtle text-success border border-success-subtle"),
            "completed" => ("Hoàn thành", "bg-primary-subtle text-primary border border-primary-subtle"),
            "pending" => ("Chờ xác nhận", "bg-warning-subtle text-warning border border-warning-subtle"),
            "cancelled_by_user" => ("Đã hủy", "bg-danger-subtle text-danger border border-danger-subtle"),
            "cancelled_by_owner" => ("Đã hủy", "bg-danger-subtle text-danger border border-danger-subtle"),
            "no_show" => ("Không đến", "bg-secondary-subtle text-secondary border border-secondary-subtle"),
            _ => (status, "bg-secondary-subtle text-secondary border border-secondary-subtle")
        };
    }

    private static string GetPaymentStatusLabel(string? paymentStatus)
    {
        return paymentStatus switch
        {
            "unpaid" => "Chưa thanh toán",
            "partially_paid" => "Thanh toán một phần",
            "fully_paid" => "Đã thanh toán",
            "refunded" => "Đã hoàn tiền",
            _ => "Chưa xác định"
        };
    }
}