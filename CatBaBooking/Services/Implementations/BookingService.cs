using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Booking;

namespace CatBaBooking.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRestaurantTableRepository _tableRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IRestaurantTableRepository tableRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _tableRepository = tableRepository;
    }

    public async Task<BookingDetailViewModel?> GetBookingDetailAsync(int bookingId, int requesterId)
    {
        // TODO:
        // 1. Lấy booking: _bookingRepository.GetByIdAsync(bookingId)
        // 2. Kiểm tra booking.UserId == requesterId (hoặc là Owner/Admin)
        // 3. Map entity → BookingDetailViewModel
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookingHistoryViewModel>> GetBookingHistoryAsync(int userId)
    {
        // TODO: _bookingRepository.GetByUserIdAsync(userId) → map → list BookingHistoryViewModel
        throw new NotImplementedException();
    }

    public async Task<(bool Success, int BookingId, string ErrorMessage)> CreateHomestayBookingAsync(BookingFormViewModel model, int userId)
    {
        // TODO:
        // 1. Kiểm tra phòng còn trống: _roomRepository.GetAvailableRoomsAsync(...)
        // 2. Nếu không còn → return (false, 0, "Phòng đã được đặt")
        // 3. Tính tổng tiền
        // 4. Tạo Booking entity
        // 5. Tạo BookedRoom entities
        // 6. _bookingRepository.CreateAsync(booking)
        // 7. return (true, booking.BookingId, "")
        throw new NotImplementedException();
    }

    public async Task<(bool Success, int BookingId, string ErrorMessage)> CreateRestaurantBookingAsync(BookingFormViewModel model, int userId)
    {
        // TODO: Tương tự nhưng kiểm tra TableAvailability, tạo BookedTable + BookingDish
        throw new NotImplementedException();
    }

    public async Task<bool> CancelBookingAsync(int bookingId, int userId)
    {
        // TODO:
        // 1. Lấy booking, kiểm tra UserId == userId
        // 2. Kiểm tra status có cho phép hủy không (không hủy nếu đã completed)
        // 3. _bookingRepository.CancelAsync(bookingId)
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateBookingStatusAsync(int bookingId, string status, int ownerId)
    {
        // TODO: Kiểm tra booking thuộc về business của ownerId → update status
        throw new NotImplementedException();
    }
}
