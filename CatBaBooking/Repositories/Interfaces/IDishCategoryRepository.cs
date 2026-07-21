using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IDishCategoryRepository
{
    List<DishCategory> GetByBusinessId(int businessId);
    bool ExistsByName(int businessId, string name);
    void Add(DishCategory category);
}