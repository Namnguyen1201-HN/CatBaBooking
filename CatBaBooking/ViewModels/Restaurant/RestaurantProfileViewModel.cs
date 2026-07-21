namespace CatBaBooking.ViewModels
{
    public class RestaurantProfileViewModel
    {
        public int BusinessId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int? AreaId { get; set; }
        public TimeOnly? OpeningHour { get; set; }
        public TimeOnly? ClosingHour { get; set; }

        // Danh sách khu vực để đổ vào dropdown
        public List<AreaOptionViewModel> AllAreas { get; set; } = new();
    }

    public class AreaOptionViewModel
    {
        public int AreaId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}