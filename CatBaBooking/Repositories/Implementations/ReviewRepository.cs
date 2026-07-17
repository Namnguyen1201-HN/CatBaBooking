using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class ReviewRepository : IReviewRepository
{
    private readonly CatbabookingContext _context;

    public ReviewRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Review>> GetByBusinessIdAsync(int businessId)
    {
        // TODO: .Where(r => r.BusinessId == businessId).Include(r => r.User).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<Review?> GetByUserAndBusinessAsync(int userId, int businessId)
    {
        // TODO: Dùng để kiểm tra user đã review chưa trước khi cho review mới
        throw new NotImplementedException();
    }

    public Task<Review> CreateAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int reviewId)
    {
        throw new NotImplementedException();
    }
}
