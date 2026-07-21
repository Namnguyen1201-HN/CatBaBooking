using CatBaBooking.Models;
using System.Collections.Generic;

namespace CatBaBooking.Repository.Interface;

public interface IBookingRepository
{
    // Lấy toàn bộ booking thuộc về 1 business (dùng cho Owner quản lý)
    List<Booking> GetByBusinessId(int businessId);

    // Lấy booking kèm đầy đủ thông tin bàn/món ăn/thanh toán (dùng cho trang Lịch sử Đặt bàn)
    List<BookingDetailDto> GetDetailedByBusinessId(int businessId);

    // Tạo booking mới kèm danh sách món ăn
    Booking? CreateBooking(Booking booking, List<BookingDish> dishes);

    // Lấy booking theo mã code (dùng để tra cứu/xác nhận)
    Booking? GetBookingByCode(string bookingCode);
}