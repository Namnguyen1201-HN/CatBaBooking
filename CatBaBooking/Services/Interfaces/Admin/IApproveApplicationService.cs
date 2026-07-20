using System.Collections.Generic;
using CatBaBooking.Models;

namespace CatBaBooking.Service.Interface.Admin;

public interface IApproveApplicationService
{
    List<Business> GetPendingApplications();

    Business? GetApplicationDetail(int businessId);

    bool ApproveApplication(int businessId);

    bool RejectApplication(int businessId, string reason);
}
