using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IApproveApplicationRepository
{
    List<Business> GetPendingBusinesses();

    Business? GetPendingBusinessById(int businessId);

    bool UpdateBusinessAndOwnerStatus(int businessId, string businessStatus, string ownerStatus);
}
