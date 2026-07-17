using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class BusinessRepository : IBusinessRepository
{
    private readonly CatbabookingContext _context;

    public BusinessRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public bool AnyBusinessByOwnerId(int ownerId)
    {
        return _context.Businesses.Any(x => x.OwnerId == ownerId);
    }

    public bool AddBusiness(Business business)
    {
        _context.Businesses.Add(business);
        _context.SaveChanges();
        return true;
    }
}
