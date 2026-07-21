using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IBusinessRepository
{
    bool AnyBusinessByOwnerId(int ownerId);
    bool AddBusiness(Business business);

    // Lấy business (nhà hàng/homestay) đầu tiên thuộc về 1 owner - dùng cho Owner Dashboard
    Business? GetByOwnerId(int ownerId);

    // Cập nhật thông tin business (dùng cho trang Thông tin Nhà hàng)
    void Update(Business business);
}