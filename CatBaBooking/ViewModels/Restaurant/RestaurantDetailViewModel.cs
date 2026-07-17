using CatBaBooking.ViewModels.Homestay;

namespace CatBaBooking.ViewModels.Restaurant;

/// <summary>ViewModel trang chi tiết Restaurant.</summary>
public class RestaurantDetailViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
    public string? CuisineType { get; set; }

    /// <summary>Danh sách bàn hiển thị trong trang detail.</summary>
    public IEnumerable<TableSummaryViewModel> Tables { get; set; } = new List<TableSummaryViewModel>();

    /// <summary>Menu món ăn, nhóm theo DishCategory.</summary>
    public IEnumerable<DishCategoryViewModel> MenuByCategory { get; set; } = new List<DishCategoryViewModel>();

    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public IEnumerable<ReviewSummaryViewModel> RecentReviews { get; set; } = new List<ReviewSummaryViewModel>();
}

public class TableSummaryViewModel
{
    public int TableId { get; set; }
    public string? TableName { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
}

public class DishCategoryViewModel
{
    public string CategoryName { get; set; } = "";
    public IEnumerable<DishItemViewModel> Dishes { get; set; } = new List<DishItemViewModel>();
}

public class DishItemViewModel
{
    public int DishId { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
