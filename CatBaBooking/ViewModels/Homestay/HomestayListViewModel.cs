namespace CatBaBooking.ViewModels.Homestay;

public class HomestayCardViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string? ThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public string? AreaName { get; set; }
    public decimal PriceFrom { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}

public class HomestayListViewModel
{
    public IEnumerable<HomestayCardViewModel> Items { get; set; } = new List<HomestayCardViewModel>();

    //paging
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    //Filters
    public int? AreaId { get; set; }
    public string? PriceRange { get; set; }
    public List<int>? MinRating { get; set; } = new List<int>();
    public List<int>? AmenityIds { get; set; } = new List<int>();
    public string? SortOrder { get; set; }

    // Dynamic Filter Data
    public Dictionary<int, string> AvailableAreas { get; set; } = new Dictionary<int, string>();
    public Dictionary<int, string> AvailableAmenities { get; set; } = new Dictionary<int, string>();
    public Dictionary<string, string> AvailablePriceRanges { get; set; } = new Dictionary<string, string>();
}
