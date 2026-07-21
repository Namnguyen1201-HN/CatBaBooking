namespace CatBaBooking.ViewModels
{
    public class RestaurantManageDishesViewModel
    {
        public string RestaurantName { get; set; } = string.Empty;
        public List<OwnerDishItemViewModel> Dishes { get; set; } = new();
        public List<CategoryItemViewModel> Categories { get; set; } = new();
    }

    public class OwnerDishItemViewModel
    {
        public int DishId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class CategoryItemViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}