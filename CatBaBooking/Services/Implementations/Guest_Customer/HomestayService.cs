using CatBaBooking.Repository.Interface;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Models;
using Microsoft.Extensions.Configuration;

namespace CatBaBooking.Services.Implementations.Guest_Customer
{
    public class HomestayService : IHomestayService 
    {
        private readonly IBusinessRepository _businessRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IConfiguration _configuration;
        private const int PageSize = 6; 

        public HomestayService(IBusinessRepository businessRepo, IBookingRepository bookingRepo, IConfiguration configuration)
        {
            _businessRepo = businessRepo;
            _bookingRepo = bookingRepo;
            _configuration = configuration;
        }

        public HomestayListViewModel GetHomestays(int page, 
                                                int? areaId = null,                                            
                                                string? priceRange = null, 
                                                List<int>? minRating = null, 
                                                List<int>? amenityIds = null, 
                                                string? sortOrder = null)
        {
            if (page < 1)
            {
                page = 1;
            }

            int totalCount;
            var homestays = _businessRepo.GetHomestays(page, PageSize, out totalCount, areaId, priceRange, minRating, amenityIds, sortOrder);

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

            var areas = _businessRepo.GetAllAreas().ToDictionary(a => a.AreaId, a => a.Name);
            var amenities = _businessRepo.GetAllAmenities().ToDictionary(a => a.AmenityId, a => a.Name);
            var priceRanges = _configuration.GetSection("AppSettings:PriceRanges").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();

            return new HomestayListViewModel
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = PageSize,
                AreaId = areaId,
                PriceRange = priceRange,
                MinRating = minRating ?? new List<int>(),
                AmenityIds = amenityIds ?? new List<int>(),
                SortOrder = sortOrder,
                AvailableAreas = areas,
                AvailableAmenities = amenities,
                AvailablePriceRanges = priceRanges
            };
        }

        public HomestayDetailViewModel GetHomestayDetail(int businessId)
        {
            var homestay = _businessRepo.GetHomestayDetail(businessId);
            if (homestay == null) return null;

            var rooms = homestay.Rooms.Select(r => new RoomSummaryViewModel
            {
                RoomId = r.RoomId,
                RoomName = r.Name,
                PricePerNight = r.PricePerNight,
                Capacity = r.Capacity,
                IsAvailable = r.IsActive ?? true
            }).ToList();

            var reviews = homestay.Reviews.Select(r => new ReviewSummaryViewModel
            {
                UserName = r.User?.FullName ?? "Khách ẩn danh",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt ?? DateTime.Now
            }).ToList();

            var amenities = homestay.Amenities.Select(a => a.Name).ToList();
            return new HomestayDetailViewModel
            {
                BusinessId = homestay.BusinessId,
                Name = homestay.Name,
                Description = homestay.Description,
                Address = homestay.Address,
                AreaName = homestay.Area?.Name,
                ThumbnailUrl = homestay.Image,
                OwnerName = homestay.Owner?.FullName ?? "Chủ homestay",
                AverageRating = (double)(homestay.AvgRating ?? 0),
                ReviewCount = homestay.ReviewCount ?? 0,
                Rooms = rooms,
                RecentReviews = reviews,
                Amenities = amenities
            };
        }

        public BookingHomestayViewModel GetBookingInfo(int roomId, int businessId)
        {
            var homestay = _businessRepo.GetHomestayDetail(businessId);
            if (homestay == null) return null;

            var room = homestay.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room == null) return null;

            return new BookingHomestayViewModel
            {
                RoomId = room.RoomId,
                BusinessId = homestay.BusinessId,
                RoomName = room.Name,
                HomestayName = homestay.Name,
                Address = homestay.Address,
                PricePerNight = room.PricePerNight,
                ThumbnailUrl = homestay.Image,
                MaxCapacity = room.Capacity
            };
        }

        public string? PlaceBooking(CheckoutHomestayViewModel model, int? userId)
        {
            var booking = new Booking
            {
                BookingCode = "HOM" + DateTime.Now.ToString("yyyyMMddHHmmss") + (userId?.ToString() ?? "G"),
                UserId = userId,
                BusinessId = model.BusinessId,
                BookerName = model.BookerName,
                BookerEmail = model.BookerEmail,
                BookerPhone = model.BookerPhone,
                NumGuests = model.NumGuests,
                TotalPrice = model.TotalAmount,
                Notes = model.Notes,
                ReservationDate = model.CheckIn, 
                ReservationEndTime = model.CheckOut.ToDateTime(TimeOnly.MinValue), 
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            var bookedRoom = new BookedRoom
            {
                RoomId = model.RoomId,
                PriceAtBooking = model.PricePerNight
            };

            booking.BookedRooms.Add(bookedRoom);

            var createdBooking = _bookingRepo.CreateBooking(booking, null); 
            return createdBooking?.BookingCode;
        }

    }
}
