using System.Collections.Generic;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface.Admin;

namespace CatBaBooking.Service.Admin;

public class ApproveApplicationService : IApproveApplicationService
{
    private readonly IApproveApplicationRepository _approveApplicationRepository;

    public ApproveApplicationService(IApproveApplicationRepository approveApplicationRepository)
    {
        _approveApplicationRepository = approveApplicationRepository;
    }

    public List<Business> GetPendingApplications()
    {
        return _approveApplicationRepository.GetPendingBusinesses();
    }

    public Business? GetApplicationDetail(int businessId)
    {
        return _approveApplicationRepository.GetPendingBusinessById(businessId);
    }

    public bool ApproveApplication(int businessId)
    {
        return _approveApplicationRepository.UpdateBusinessAndOwnerStatus(businessId, "active", "active");
    }

    public bool RejectApplication(int businessId, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            return false;
        }

        // Lưu ý: bảng businesses/users hiện chưa có cột lưu lý do từ chối,
        // nên "reason" ở đây chỉ được validate là không rỗng, chưa được lưu vào DB.
        // Nếu cần lưu lại, hãy thêm cột reject_reason (nullable) vào bảng businesses.
        return _approveApplicationRepository.UpdateBusinessAndOwnerStatus(businessId, "rejected", "rejected");
    }
}
