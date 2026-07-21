using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class AreaRepository : IAreaRepository
{
    private readonly CatbabookingContext _context;

    public AreaRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public List<Area> GetAll()
    {
        return _context.Areas.OrderBy(a => a.Name).ToList();
    }
}