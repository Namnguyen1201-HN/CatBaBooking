using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class DishRepository : IDishRepository
{
    private readonly CatbabookingContext _context;

    public DishRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public List<Dish> GetByBusinessId(int businessId)
    {
        return _context.Dishes
            .Where(d => d.BusinessId == businessId)
            .OrderBy(d => d.DishId)
            .ToList();
    }

    public Dish? GetById(int dishId)
    {
        return _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
    }

    public void Add(Dish dish)
    {
        _context.Dishes.Add(dish);
        _context.SaveChanges();
    }

    public void Update(Dish dish)
    {
        _context.Dishes.Update(dish);
        _context.SaveChanges();
    }
}