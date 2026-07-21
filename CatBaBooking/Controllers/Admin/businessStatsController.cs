using System;
using CatBaBooking.Helpers;
using CatBaBooking.Service.Interface.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Admin;

[Route("business-stats")]
public class businessStatsController : Controller
{
    private readonly IBusinessStatsService _businessStatsService;
    private const int PageSize = 10;

    public businessStatsController(IBusinessStatsService businessStatsService)
    {
        _businessStatsService = businessStatsService;
    }

    // GET /business-stats?type=&sort=&page=
    public IActionResult Index(string? type, string? sort, int page = 1)
    {
        string? roleName = HttpContext.Session.GetString("RoleName");
        if (!string.Equals(roleName, "admin", StringComparison.OrdinalIgnoreCase))
        {
            return Redirect("/login");
        }

        var (rows, totalCount) = _businessStatsService.GetBusinessStats(type, sort, page, PageSize);

        ViewBag.Rows = rows;
        ViewBag.TotalCount = totalCount;
        ViewBag.CurrentPage = PaginationHelper.NormalizePage(page, PaginationHelper.GetTotalPages(totalCount, PageSize));
        ViewBag.TotalPages = PaginationHelper.GetTotalPages(totalCount, PageSize);

        ViewBag.Type = type;
        ViewBag.Sort = sort;

        return View("~/Views/Admin/BusinessStats.cshtml");
    }
}
