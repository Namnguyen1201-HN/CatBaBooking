using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;

namespace CatBaBooking.Repositories.Implementations;

public class RoomRepository : IRoomRepository
{
    private readonly CatbabookingContext _context;

    public RoomRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public Task<Room?> GetByIdAsync(int roomId)
    {
        // TODO: .Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.RoomId == roomId)
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> GetByHomestayIdAsync(int businessId)
    {
        // TODO: .Where(r => r.BusinessId == businessId).Include(r => r.RoomImages).ToListAsync()
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> GetAvailableRoomsAsync(int businessId, DateOnly checkIn, DateOnly checkOut)
    {
        // TODO: Lấy phòng không bị trùng lịch với BookedRooms trong khoảng checkIn-checkOut
        // Gợi ý dùng: .Where(r => !r.BookedRooms.Any(br => br.CheckIn < checkOut && br.CheckOut > checkIn))
        throw new NotImplementedException();
    }

    public Task<Room> CreateAsync(Room room)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Room room)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int roomId)
    {
        throw new NotImplementedException();
    }
}
