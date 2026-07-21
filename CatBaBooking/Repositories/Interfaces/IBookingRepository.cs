using CatBaBooking.Models;
using System.Collections.Generic;

namespace CatBaBooking.Repositories.Interfaces
{
namespace CatBaBooking.Repository.Interface;

    public interface IBookingRepository
    {
        Booking? CreateBooking(Booking booking, List<BookingDish> dishes);
        Booking? GetBookingByCode(string bookingCode);
    }
    // Lấy toàn bộ booking thuộc về 1 business (dùng cho Owner quản lý)
    List<Booking> GetByBusinessId(int businessId);

    // Lấy booking kèm đầy đủ thông tin bàn/món ăn/thanh toán (dùng cho trang Lịch sử Đặt bàn)
    List<BookingDetailDto> GetDetailedByBusinessId(int businessId);
}
