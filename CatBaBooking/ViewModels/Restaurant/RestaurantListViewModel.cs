namespace CatBaBooking.ViewModels.Restaurant;


public class RestaurantCardViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? AreaName { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public string? CuisineType { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
    public decimal? AveragePricePerPerson { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}


public class RestaurantListViewModel
{
    public IEnumerable<RestaurantCardViewModel> Items { get; set; } = new List<RestaurantCardViewModel>();
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    // Filters
    public int? AreaId { get; set; }
    public string? RestaurantType { get; set; }
    public List<int>? MinRating { get; set; } = new List<int>();
    public string? SortOrder { get; set; }
}
