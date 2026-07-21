using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IDishRepository
{
    List<Dish> GetByBusinessId(int businessId);
    Dish? GetById(int dishId);
    void Add(Dish dish);
    void Update(Dish dish);
}