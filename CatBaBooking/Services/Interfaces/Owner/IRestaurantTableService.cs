using CatBaBooking.ViewModels;

namespace CatBaBooking.Service.Interface;

public interface IRestaurantTableService
{
    RestaurantManageTablesViewModel GetTables(int ownerId);

    (bool Success, string Message) AddTable(int ownerId, string name, int capacity, bool isActive);
    (bool Success, string Message) UpdateTable(int ownerId, int tableId, string name, int capacity, bool isActive);
    (bool Success, string Message) DeleteTable(int ownerId, int tableId);
}