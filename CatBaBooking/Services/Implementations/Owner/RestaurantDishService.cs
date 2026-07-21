using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;

namespace CatBaBooking.Service;

public class RestaurantDishService : IRestaurantDishService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IDishCategoryRepository _categoryRepository;

    public RestaurantDishService(
        IBusinessRepository businessRepository,
        IDishRepository dishRepository,
        IDishCategoryRepository categoryRepository)
    {
        _businessRepository = businessRepository;
        _dishRepository = dishRepository;
        _categoryRepository = categoryRepository;
    }

    public RestaurantManageDishesViewModel GetDishesPageData(int ownerId)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return new RestaurantManageDishesViewModel();
        }

        var categories = _categoryRepository.GetByBusinessId(business.BusinessId);
        var categoryNameById = categories.ToDictionary(c => c.CategoryId, c => c.Name);

        var dishes = _dishRepository.GetByBusinessId(business.BusinessId)
            .Select(d => new OwnerDishItemViewModel
            {
                DishId = d.DishId,
                Name = d.Name,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.CategoryId.HasValue && categoryNameById.TryGetValue(d.CategoryId.Value, out var name)
                    ? name
                    : "(Chưa phân loại)",
                Description = d.Description,
                ImageUrl = d.ImageUrl,
                IsAvailable = d.IsAvailable
            })
            .ToList();

        return new RestaurantManageDishesViewModel
        {
            RestaurantName = business.Name,
            Dishes = dishes,
            Categories = categories.Select(c => new CategoryItemViewModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            }).ToList()
        };
    }

    public (bool Success, string Message) AddDish(
        int ownerId, string name, decimal price, int categoryId, string? description, string? imageUrl, bool isAvailable)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return (false, "Không tìm thấy nhà hàng của bạn.");
        }

        if (string.IsNullOrWhiteSpace(name) || price < 0)
        {
            return (false, "Tên món và giá không hợp lệ.");
        }

        var dish = new Dish
        {
            BusinessId = business.BusinessId,
            CategoryId = categoryId,
            Name = name.Trim(),
            Description = description?.Trim(),
            Price = price,
            ImageUrl = imageUrl,
            IsAvailable = isAvailable
        };

        _dishRepository.Add(dish);
        return (true, $"Đã thêm món \"{dish.Name}\" thành công.");
    }

    public (bool Success, string Message) UpdateDish(
        int ownerId, int dishId, string name, decimal price, int categoryId, string? description, string? imageUrl, bool isAvailable)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        var dish = _dishRepository.GetById(dishId);

        if (business == null || dish == null || dish.BusinessId != business.BusinessId)
        {
            return (false, "Không tìm thấy món ăn hoặc bạn không có quyền chỉnh sửa món này.");
        }

        if (string.IsNullOrWhiteSpace(name) || price < 0)
        {
            return (false, "Tên món và giá không hợp lệ.");
        }

        dish.Name = name.Trim();
        dish.Price = price;
        dish.CategoryId = categoryId;
        dish.Description = description?.Trim();
        dish.IsAvailable = isAvailable;

        // Chỉ đổi ảnh nếu có ảnh mới; giữ nguyên nếu imageUrl null (Controller truyền null khi không upload file mới)
        if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            dish.ImageUrl = imageUrl;
        }

        _dishRepository.Update(dish);
        return (true, $"Đã cập nhật món \"{dish.Name}\" thành công.");
    }

    public (bool Success, string Message) AddCategory(int ownerId, string name)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return (false, "Không tìm thấy nhà hàng của bạn.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return (false, "Tên danh mục không được để trống.");
        }

        if (_categoryRepository.ExistsByName(business.BusinessId, name.Trim()))
        {
            return (false, $"Danh mục \"{name.Trim()}\" đã tồn tại.");
        }

        _categoryRepository.Add(new DishCategory
        {
            BusinessId = business.BusinessId,
            Name = name.Trim(),
            DisplayOrder = 0
        });

        return (true, $"Đã thêm danh mục \"{name.Trim()}\" thành công.");
    }
}