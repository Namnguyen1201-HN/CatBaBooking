using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IBusinessRepository
{
    bool AnyBusinessByOwnerId(int ownerId);
    bool AddBusiness(Business business);
}
