namespace CatBaBooking.ViewModels
{
    public class RestaurantManageTablesViewModel
    {
        public string RestaurantName { get; set; } = string.Empty;
        public List<TableItemViewModel> Tables { get; set; } = new();
    }

    public class TableItemViewModel
    {
        public int TableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsActive { get; set; } = true;
    }
}