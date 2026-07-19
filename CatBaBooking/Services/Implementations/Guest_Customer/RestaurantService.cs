using CatBaBooking.Repository.Interface;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;
using CatBaBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Services.Implementations.Guest_Customer
{
    public class RestaurantService : IRestaurantService //NamNS
    {
        private readonly IBusinessRepository _businessRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IBookingRepository _bookingRepo;
        private const int PageSize = 6; 

        public RestaurantService(IBusinessRepository businessRepo, ICartRepository cartRepo, IBookingRepository bookingRepo)
        {
            _businessRepo = businessRepo;
            _cartRepo = cartRepo;
            _bookingRepo = bookingRepo;
        }

        public RestaurantListViewModel GetRestaurants(int page, int? areaId = null, string? restaurantType = null, List<int>? minRating = null, string? sortOrder = null)
        {
            if (page < 1) page = 1;
            int totalCount;
            var restaurants = _businessRepo.GetRestaurants(page, PageSize, out totalCount, areaId, restaurantType, minRating, sortOrder);
            var items = restaurants.Select(r => new RestaurantCardViewModel
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
            return new RestaurantListViewModel
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = PageSize,
                AreaId = areaId,
                RestaurantType = restaurantType,
                MinRating = minRating ?? new List<int>(),
                SortOrder = sortOrder
            };
        }

        public RestaurantDetailViewModel GetRestaurantDetail(int businessId)
        {
            var restaurant = _businessRepo.GetRestaurantDetail(businessId);
            if (restaurant == null) return null;
            var tables = restaurant.RestaurantTables.Select(t => new TableSummaryViewModel
            {
                TableId = t.TableId,
                TableName = t.Name,
                Capacity = t.Capacity,
                IsAvailable = t.IsActive ?? true
            }).ToList();
            // Nhóm các món ăn theo từng Category
            var menu = restaurant.DishCategories.Select(c => new DishCategoryViewModel
            {
                CategoryName = c.Name,
                Dishes = restaurant.Dishes.Where(d => d.CategoryId == c.CategoryId).Select(d => new DishItemViewModel
                {
                    DishId = d.DishId,
                    Name = d.Name,
                    Price = d.Price,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl
                }).ToList()
            }).ToList();
            // Gom các món chưa có danh mục vào nhóm "Khác"
            var uncategorized = restaurant.Dishes.Where(d => d.CategoryId == null).ToList();
            if (uncategorized.Any())
            {
                menu.Add(new DishCategoryViewModel
                {
                    CategoryName = "Khác",
                    Dishes = uncategorized.Select(d => new DishItemViewModel
                    {
                        DishId = d.DishId,
                        Name = d.Name,
                        Price = d.Price,
                        Description = d.Description,
                        ImageUrl = d.ImageUrl
                    }).ToList()
                });
            }
            var reviews = restaurant.Reviews.Select(r => new ReviewSummaryViewModel
            {
                UserName = r.User?.FullName ?? "Khách ẩn danh",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt ?? DateTime.Now
            }).ToList();
            return new RestaurantDetailViewModel
            {
                BusinessId = restaurant.BusinessId,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                Phone = "028 3456 7899", // Fake 
                ThumbnailUrl = restaurant.Image,
                OpeningTime = restaurant.OpeningHour?.ToString("HH:mm"),
                ClosingTime = restaurant.ClosingHour?.ToString("HH:mm"),
                AverageRating = (double)(restaurant.AvgRating ?? 0),
                ReviewCount = restaurant.ReviewCount ?? 0,
                Tables = tables,
                MenuByCategory = menu,
                RecentReviews = reviews
            };
        }


        public CheckoutRestaurantViewModel GetCheckoutInfo(int businessId, int? userId, List<CartItemViewModel> guestCartItems = null)
        {
            var business = _businessRepo.GetRestaurantDetail(businessId);
            if (business == null) return null;

            var cartItems = new List<CartItemViewModel>();
            if (userId.HasValue)
            {
                var dbCarts = _cartRepo.GetCartItems(businessId, userId.Value);
                cartItems = dbCarts.Select(c => new CartItemViewModel
                {
                    DishId = c.DishId,
                    DishName = c.Dish.Name,
                    ImageUrl = c.Dish.ImageUrl,
                    Quantity = c.Quantity,
                    Price = c.Dish.Price
                }).ToList();
            }
            else if (guestCartItems != null)
            {
                cartItems = guestCartItems;
            }

            var model = new CheckoutRestaurantViewModel
            {
                BusinessId = business.BusinessId,
                RestaurantName = business.Name,
                RestaurantAddress = business.Address,
                CartItems = cartItems,
                TotalAmount = cartItems.Sum(c => c.Subtotal)
            };

            return model;
        }

        public string? PlaceBooking(CheckoutRestaurantViewModel model, int? userId, List<CartItemViewModel> guestCartItems = null)
        {
            var bookingDishes = new List<BookingDish>();
            decimal computedTotal = 0;
            
            if (userId.HasValue)
            {
                var dbCarts = _cartRepo.GetCartItems(model.BusinessId, userId.Value);
                foreach (var item in dbCarts)
                {
                    bookingDishes.Add(new BookingDish
                    {
                        DishId = item.DishId,
                        Quantity = item.Quantity,
                        PriceAtBooking = item.Subtotal / item.Quantity
                    });
                    computedTotal += item.Subtotal;
                }
            }
            else if (guestCartItems != null)
            {
                foreach (var item in guestCartItems)
                {
                    bookingDishes.Add(new BookingDish
                    {
                        DishId = item.DishId,
                        Quantity = item.Quantity,
                        PriceAtBooking = item.Price
                    });
                    computedTotal += item.Quantity * item.Price;
                }
            }

            var booking = new Booking
            {
                BookingCode = "RES" + DateTime.Now.ToString("yyyyMMddHHmmss") + (userId?.ToString() ?? "G"),
                UserId = userId,
                BusinessId = model.BusinessId,
                BookerName = model.BookerName,
                BookerEmail = model.BookerEmail,
                BookerPhone = model.BookerPhone,
                NumGuests = model.NumGuests,
                TotalPrice = computedTotal,
                Notes = model.SpecialRequests,
                ReservationDate = model.ReservationDate,
                ReservationTime = model.ReservationTime,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            var createdBooking = _bookingRepo.CreateBooking(booking, bookingDishes);
            if (createdBooking != null && userId.HasValue)
            {
                _cartRepo.ClearUserCart(model.BusinessId, userId.Value);
            }

            return createdBooking?.BookingCode;
        }
    }
}
