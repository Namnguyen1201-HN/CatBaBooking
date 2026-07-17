using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

/// <summary>
/// Triển khai IBusinessRepository.
/// Business bao gồm cả Homestay và Restaurant — phân biệt qua BusinessType.
/// </summary>
public class BusinessRepository : IBusinessRepository
{
    private readonly CatbabookingContext _context;

    public BusinessRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Business>> GetAllActiveAsync()
    {
        // TODO: Lấy tất cả business có Status = "approved"
        // Gợi ý: .Where(b => b.Status == "approved").ToListAsync()
        throw new NotImplementedException();
    }

    public Task<Business?> GetByIdAsync(int businessId)
    {
        // TODO: .Include(b => b.Amenities).Include(b => b.Reviews) ... .FindAsync(businessId)
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetByOwnerIdAsync(int ownerId)
    {
        // TODO: .Where(b => b.OwnerId == ownerId).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetPendingAsync()
    {
        // TODO: .Where(b => b.Status == "pending").ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetHomestaysAsync(int page, int pageSize)
    {
        // TODO: .Where(b => b.BusinessType == "Homestay")
        //        .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Business>> GetRestaurantsAsync(int page, int pageSize)
    {
        // TODO: .Where(b => b.BusinessType == "Restaurant")
        //        .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<int> CountHomestaysAsync()
    {
        // TODO: .CountAsync(b => b.BusinessType == "Homestay" && b.Status == "approved")
        throw new NotImplementedException();
    }

    public Task<int> CountRestaurantsAsync()
    {
        // TODO: .CountAsync(b => b.BusinessType == "Restaurant" && b.Status == "approved")
        throw new NotImplementedException();
    }

    public Task<Business> CreateAsync(Business business)
    {
        // TODO: _context.Businesses.Add(business); SaveChanges; return business;
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Business business)
    {
        // TODO: _context.Businesses.Update(business); SaveChanges;
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int businessId, string status)
    {
        // TODO: Tìm business, đổi Status, SaveChanges
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int businessId)
    {
        // TODO: Tìm business, Remove, SaveChanges
        throw new NotImplementedException();
    }
}
