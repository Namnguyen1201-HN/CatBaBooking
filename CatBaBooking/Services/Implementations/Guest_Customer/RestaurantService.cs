using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces.Guest_Customer;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Implementations.Guest_Customer
{
    public class RestaurantService : IRestaurantService //NamNS
    {
        private readonly IBusinessRepository _businessRepo;
        private const int PageSize = 6; 

        public RestaurantService(IBusinessRepository businessRepo)
        {
            _businessRepo = businessRepo;
        }

        public RestaurantListViewModel GetRestaurants(int page)
        {
            if (page < 1) page = 1;
            int totalCount;
            var restaurants = _businessRepo.GetRestaurants(page, PageSize, out totalCount);
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
                PageSize = PageSize
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

    }
}
