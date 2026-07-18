using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;

namespace CatBaBooking.Services.Implementations.Guest_Customer
{
    public class HomestayService : IHomestayService //NamNS
    {
        private readonly IBusinessRepository _businessRepo;
        private const int PageSize = 6; 

        public HomestayService(IBusinessRepository businessRepo)
        {
            _businessRepo = businessRepo;
        }

        public HomestayListViewModel GetHomestays(int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            int totalCount;
            var homestays = _businessRepo.GetHomestays(page, PageSize, out totalCount);

            var items = homestays.Select(h => new HomestayCardViewModel
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

            return new HomestayListViewModel
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = PageSize
            };
        }

        public HomestayDetailViewModel GetHomestayDetail(int businessId)
        {
            var homestay = _businessRepo.GetHomestayDetail(businessId);
            if (homestay == null) return null;

            // Map danh sách phòng
            var rooms = homestay.Rooms.Select(r => new RoomSummaryViewModel
            {
                RoomId = r.RoomId,
                RoomName = r.Name,
                PricePerNight = r.PricePerNight,
                Capacity = r.Capacity,
                IsAvailable = r.IsActive ?? true
            }).ToList();

            // Map danh sách đánh giá
            var reviews = homestay.Reviews.Select(r => new ReviewSummaryViewModel
            {
                UserName = r.User?.FullName ?? "Khách ẩn danh",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt ?? DateTime.Now
            }).ToList();

            // Map danh sách tiện nghi
            var amenities = homestay.Amenities.Select(a => a.Name).ToList();
            return new HomestayDetailViewModel
            {
                BusinessId = homestay.BusinessId,
                Name = homestay.Name,
                Description = homestay.Description,
                Address = homestay.Address,
                AreaName = homestay.Area?.Name,
                ThumbnailUrl = homestay.Image,
                AverageRating = (double)(homestay.AvgRating ?? 0),
                ReviewCount = homestay.ReviewCount ?? 0,
                Rooms = rooms,
                RecentReviews = reviews,
                Amenities = amenities
            };
        }

    }
}
