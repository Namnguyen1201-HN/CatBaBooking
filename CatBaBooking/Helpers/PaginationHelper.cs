using System;
using System.Collections.Generic;

namespace CatBaBooking.Helpers;

public static class PaginationHelper
{
    /// <summary>Tính tổng số trang từ tổng số bản ghi và kích thước trang.</summary>
    public static int GetTotalPages(int totalCount, int pageSize)
    {
        if (pageSize <= 0) return 0;
        return (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    /// <summary>Đảm bảo page nằm trong khoảng [1, totalPages] (hoặc 1 nếu totalPages = 0).</summary>
    public static int NormalizePage(int page, int totalPages)
    {
        if (page < 1) return 1;
        if (totalPages > 0 && page > totalPages) return totalPages;
        return page;
    }

    /// <summary>
    /// Tạo danh sách các "token" để render thanh phân trang: số trang dạng string, hoặc "..." để rút gọn.
    /// Ví dụ với currentPage=5, totalPages=20: 1, ..., 4, 5, 6, ..., 20
    /// </summary>
    public static List<string> BuildPageItems(int currentPage, int totalPages, int siblingCount = 1)
    {
        var items = new List<string>();
        if (totalPages <= 0) return items;

        int start = Math.Max(1, currentPage - siblingCount);
        int end = Math.Min(totalPages, currentPage + siblingCount);

        items.Add("1");
        if (start > 2) items.Add("...");

        for (int p = Math.Max(2, start); p <= Math.Min(totalPages - 1, end); p++)
        {
            items.Add(p.ToString());
        }

        if (end < totalPages - 1) items.Add("...");
        if (totalPages > 1) items.Add(totalPages.ToString());

        return items;
    }
}
