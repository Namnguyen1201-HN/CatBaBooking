using CatBaBooking.ViewModels.Homestay;

namespace CatBaBooking.Services.Interfaces.Guest_Customer
{
    public interface IHomestayService 
    {   
        HomestayListViewModel GetHomestays(int page, 
                                            int? areaId = null,                                        
                                            string? priceRange = null, 
                                            List<int>? minRating = null, 
                                            List<int>? amenityIds = null, 
                                            string? sortOrder = null);
        
        HomestayDetailViewModel GetHomestayDetail(int businessId);
        BookingHomestayViewModel GetBookingInfo(int roomId, int businessId);
        string? PlaceBooking(CheckoutHomestayViewModel model, int? userId);
    }
}
