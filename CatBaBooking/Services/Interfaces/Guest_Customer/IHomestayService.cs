using CatBaBooking.ViewModels.Homestay;

namespace CatBaBooking.Services.Interfaces.Guest_Customer
{
    public interface IHomestayService //NamNS
    {
        HomestayListViewModel GetHomestays(int page);
        HomestayDetailViewModel GetHomestayDetail(int businessId);
        BookingHomestayViewModel GetBookingInfo(int roomId, int businessId);
        string? PlaceBooking(CheckoutHomestayViewModel model, int? userId);
    }
}
