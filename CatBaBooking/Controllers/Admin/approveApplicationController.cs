using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("approve-application")]
public class approveApplicationController : Controller
{
    private readonly IApproveApplicationService _approveApplicationService;

    public approveApplicationController(IApproveApplicationService approveApplicationService)
    {
        _approveApplicationService = approveApplicationService;
    }

    // GET /approve-application
    public IActionResult Index()
    {
        ViewBag.PendingBusinesses = _approveApplicationService.GetPendingApplications();
        return View("~/Views/Admin/Approveapplication.cshtml");
    }

    // GET /approve-application/{id}
    [HttpGet("{id:int}")]
    public IActionResult Detail(int id)
    {
        var business = _approveApplicationService.GetApplicationDetail(id);
        if (business == null)
        {
            TempData["ErrorMessage"] = "Yêu cầu không tồn tại hoặc đã được xử lý.";
            return Redirect("/approve-application");
        }

        ViewBag.Business = business;
        return View("~/Views/Admin/ApproveApplicationDetail.cshtml");
    }

    // POST /approve-application/{id}/approve
    [HttpPost("{id:int}/approve")]
    public IActionResult Approve(int id)
    {
        bool isSuccess = _approveApplicationService.ApproveApplication(id);
        TempData[isSuccess ? "SuccessMessage" : "ErrorMessage"] =
            isSuccess ? "Đã phê duyệt yêu cầu thành công." : "Không thể phê duyệt yêu cầu này.";
        return Redirect("/approve-application");
    }

    // POST /approve-application/{id}/reject
    [HttpPost("{id:int}/reject")]
    public IActionResult Reject(int id, string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            TempData["ErrorMessage"] = "Vui lòng nhập lý do từ chối.";
            return Redirect($"/approve-application/{id}");
        }

        bool isSuccess = _approveApplicationService.RejectApplication(id, reason);
        TempData[isSuccess ? "SuccessMessage" : "ErrorMessage"] =
            isSuccess ? "Đã từ chối yêu cầu." : "Không thể từ chối yêu cầu này.";
        return Redirect("/approve-application");
    }
}
