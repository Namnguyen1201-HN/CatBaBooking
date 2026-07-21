using CatBaBooking.Helpers;
using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("feedback-management")]
public class feedbackManagementController : Controller
{
    private readonly IFeedbackService _feedbackService;
    private const int PageSize = 10;

    public feedbackManagementController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    // GET /feedback-management?search=&rating=&sort=&page=
    public IActionResult Index(string? search, int? rating, string? sort, int page = 1)
    {
        var (reviews, totalCount) = _feedbackService.GetPagedReviews(search, rating, sort, page, PageSize);

        ViewBag.Reviews = reviews;
        ViewBag.TotalCount = totalCount;
        ViewBag.CurrentPage = PaginationHelper.NormalizePage(page, PaginationHelper.GetTotalPages(totalCount, PageSize));
        ViewBag.TotalPages = PaginationHelper.GetTotalPages(totalCount, PageSize);

        ViewBag.Search = search;
        ViewBag.Rating = rating;
        ViewBag.Sort = sort;

        return View("~/Views/Admin/FeedbackManagement.cshtml");
    }

    // GET /feedback-management/{id}
    [HttpGet("{id:int}")]
    public IActionResult Detail(int id)
    {
        var review = _feedbackService.GetReviewDetail(id);
        if (review == null)
        {
            TempData["ErrorMessage"] = "Không tìm thấy phản hồi này.";
            return Redirect("/feedback-management");
        }

        ViewBag.Review = review;
        return View("~/Views/Admin/FeedbackDetail.cshtml");
    }

    // POST /feedback-management/{id}/delete
    [HttpPost("{id:int}/delete")]
    public IActionResult Delete(int id)
    {
        bool isSuccess = _feedbackService.DeleteReview(id);
        TempData[isSuccess ? "SuccessMessage" : "ErrorMessage"] =
            isSuccess ? "Đã xóa phản hồi thành công." : "Không thể xóa phản hồi này.";
        return Redirect("/feedback-management");
    }
}
