using CatBaBooking.Repository.Interface;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Implementations.Guest_Customer;

public class HomeService : IHomeService 
{
    private readonly IBusinessRepository _businessRepo;

    public HomeService(IBusinessRepository businessRepo)
    {
        _businessRepo = businessRepo;
    }

    public List<HomestayCardViewModel> GetFeaturedHomestays()
    {
        var homestays = _businessRepo.GetFeaturedHomestays(3);
        
        // Map to ViewModel
        return homestays.Select(h => new HomestayCardViewModel
        {
            BusinessId = h.BusinessId,
            Name = h.Name,
            ThumbnailUrl = h.Image,
            Address = h.Address,
            AreaName = h.Area?.Name,
            AverageRating = (double)(h.AvgRating ?? 0),
            ReviewCount = h.ReviewCount ?? 0,
            PriceFrom = h.Rooms.Any() ? h.Rooms.Min(r => r.PricePerNight) : (h.PricePerNight ?? 0)
        }).ToList();
    }

    public List<RestaurantCardViewModel> GetFeaturedRestaurants()
    {       
        var restaurants = _businessRepo.GetFeaturedRestaurants(3);
        
        return restaurants.Select(r => new RestaurantCardViewModel
        {
            BusinessId = r.BusinessId,
            Name = r.Name,
            ThumbnailUrl = r.Image,
            Address = r.Address,
            AreaName = r.Area?.Name,
            AverageRating = (double)(r.AvgRating ?? 0),
            ReviewCount = r.ReviewCount ?? 0,
            OpeningTime = r.OpeningHour?.ToString("HH:mm"),
            ClosingTime = r.ClosingHour?.ToString("HH:mm")
        }).ToList();
    }
}