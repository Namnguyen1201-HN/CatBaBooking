using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class DishCategoryRepository : IDishCategoryRepository
{
    private readonly CatbabookingContext _context;

    public DishCategoryRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public List<DishCategory> GetByBusinessId(int businessId)
    {
        return _context.DishCategories
            .Where(c => c.BusinessId == businessId)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToList();
    }

    public bool ExistsByName(int businessId, string name)
    {
        return _context.DishCategories
            .Any(c => c.BusinessId == businessId && c.Name.ToLower() == name.ToLower());
    }

    public void Add(DishCategory category)
    {
        _context.DishCategories.Add(category);
        _context.SaveChanges();
    }
}