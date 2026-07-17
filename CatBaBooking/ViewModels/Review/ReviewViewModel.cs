using System.ComponentModel.DataAnnotations;

namespace CatBaBooking.ViewModels.Review;

/// <summary>Hiển thị một review trong danh sách.</summary>
public class ReviewViewModel
{
    public int ReviewId { get; set; }
    public string UserName { get; set; } = "";
    public string? UserAvatarUrl { get; set; }
    public int Rating { get; set; }  // 1-5 sao
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>Form gửi đánh giá mới.</summary>
public class LeaveReviewViewModel
{
    public int BusinessId { get; set; }

    [Range(1, 5, ErrorMessage = "Vui lòng chọn điểm từ 1-5 sao")]
    public int Rating { get; set; }

    [StringLength(1000, ErrorMessage = "Đánh giá tối đa 1000 ký tự")]
    public string? Comment { get; set; }
}
