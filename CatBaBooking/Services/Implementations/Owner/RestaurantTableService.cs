using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;

namespace CatBaBooking.Services.Implementations.Owner;

public class RestaurantTableService : IRestaurantTableService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IRestaurantTableRepository _tableRepository;

    public RestaurantTableService(IBusinessRepository businessRepository, IRestaurantTableRepository tableRepository)
    {
        _businessRepository = businessRepository;
        _tableRepository = tableRepository;
    }

    public RestaurantManageTablesViewModel GetTables(int ownerId)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return new RestaurantManageTablesViewModel();
        }

        var tables = _tableRepository.GetByBusinessId(business.BusinessId)
            .Select(t => new TableItemViewModel
            {
                TableId = t.TableId,
                Name = t.Name,
                Capacity = t.Capacity,
                IsActive = t.IsActive ?? true
            })
            .ToList();

        return new RestaurantManageTablesViewModel
        {
            RestaurantName = business.Name,
            Tables = tables
        };
    }

    public (bool Success, string Message) AddTable(int ownerId, string name, int capacity, bool isActive)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return (false, "Không tìm thấy nhà hàng của bạn.");
        }

        if (string.IsNullOrWhiteSpace(name) || capacity <= 0)
        {
            return (false, "Tên bàn và sức chứa không hợp lệ.");
        }

        var table = new RestaurantTable
        {
            BusinessId = business.BusinessId,
            Name = name.Trim(),
            Capacity = capacity,
            IsActive = isActive
        };

        _tableRepository.Add(table);
        return (true, $"Đã thêm bàn \"{table.Name}\" thành công.");
    }

    public (bool Success, string Message) UpdateTable(int ownerId, int tableId, string name, int capacity, bool isActive)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        var table = _tableRepository.GetById(tableId);

        if (business == null || table == null || table.BusinessId != business.BusinessId)
        {
            return (false, "Không tìm thấy bàn hoặc bạn không có quyền chỉnh sửa bàn này.");
        }

        if (string.IsNullOrWhiteSpace(name) || capacity <= 0)
        {
            return (false, "Tên bàn và sức chứa không hợp lệ.");
        }

        table.Name = name.Trim();
        table.Capacity = capacity;
        table.IsActive = isActive;
        _tableRepository.Update(table);

        return (true, "Cập nhật bàn thành công.");
    }

    public (bool Success, string Message) DeleteTable(int ownerId, int tableId)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        var table = _tableRepository.GetById(tableId);

        if (business == null || table == null || table.BusinessId != business.BusinessId)
        {
            return (false, "Không tìm thấy bàn hoặc bạn không có quyền xoá bàn này.");
        }

        if (_tableRepository.HasBookings(tableId))
        {
            return (false, $"Không thể xoá bàn \"{table.Name}\" vì đã có lịch sử đặt bàn. " +
                            "Hãy chuyển trạng thái sang \"Tạm ngưng\" thay vì xoá.");
        }

        _tableRepository.Delete(table);
        return (true, $"Đã xoá bàn \"{table.Name}\" thành công.");
    }
}