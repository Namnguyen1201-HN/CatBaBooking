namespace CatBaBooking.ViewModels.Restaurant;

/// <summary>Card tóm tắt Restaurant dùng trong trang listing.</summary>
public class RestaurantCardViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? ThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public string? CuisineType { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
    public decimal? AveragePricePerPerson { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}

/// <summary>ViewModel trang danh sách Restaurant (với phân trang).</summary>
public class RestaurantListViewModel
{
    public IEnumerable<RestaurantCardViewModel> Items { get; set; } = new List<RestaurantCardViewModel>();
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
