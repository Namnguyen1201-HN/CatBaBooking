using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Interfaces;
using CatBaBooking.ViewModels.Homestay;
using CatBaBooking.ViewModels.Restaurant;

namespace CatBaBooking.Services.Implementations;

/// <summary>
/// Triển khai IBusinessService.
/// Đây là nơi map giữa entity Business (từ DB) và ViewModel (cho View).
/// </summary>
public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRestaurantTableRepository _tableRepository;
    private readonly IDishRepository _dishRepository;

    public BusinessService(
        IBusinessRepository businessRepository,
        IReviewRepository reviewRepository,
        IRoomRepository roomRepository,
        IRestaurantTableRepository tableRepository,
        IDishRepository dishRepository)
    {
        _businessRepository = businessRepository;
        _reviewRepository = reviewRepository;
        _roomRepository = roomRepository;
        _tableRepository = tableRepository;
        _dishRepository = dishRepository;
    }

    public async Task<HomestayListViewModel> GetHomestayListAsync(int page, int pageSize)
    {
        // TODO:
        // 1. Lấy danh sách homestay: _businessRepository.GetHomestaysAsync(page, pageSize)
        // 2. Đếm tổng: _businessRepository.CountHomestaysAsync()
        // 3. Map từng Business → HomestayCardViewModel
        // 4. Trả về HomestayListViewModel { Items, TotalCount, CurrentPage, PageSize }
        throw new NotImplementedException();
    }

    public async Task<HomestayDetailViewModel?> GetHomestayDetailAsync(int businessId)
    {
        // TODO:
        // 1. _businessRepository.GetByIdAsync(businessId)
        // 2. _roomRepository.GetByHomestayIdAsync(businessId)
        // 3. _reviewRepository.GetByBusinessIdAsync(businessId)
        // 4. Map tất cả vào HomestayDetailViewModel
        throw new NotImplementedException();
    }

    public async Task<RestaurantListViewModel> GetRestaurantListAsync(int page, int pageSize)
    {
        // TODO: Tương tự GetHomestayListAsync
        throw new NotImplementedException();
    }

    public async Task<RestaurantDetailViewModel?> GetRestaurantDetailAsync(int businessId)
    {
        // TODO: Gồm tables, dishes, reviews
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Business>> GetOwnerBusinessesAsync(int ownerId)
    {
        // TODO: return await _businessRepository.GetByOwnerIdAsync(ownerId);
        throw new NotImplementedException();
    }

    public async Task<Business> CreateBusinessAsync(Business business)
    {
        // TODO: Đặt status = "pending" trước khi tạo (Admin cần phê duyệt)
        // business.Status = "pending";
        // return await _businessRepository.CreateAsync(business);
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateBusinessAsync(Business business, int ownerId)
    {
        // TODO: Kiểm tra business.OwnerId == ownerId (tránh Owner sửa listing của người khác)
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBusinessAsync(int businessId, int requesterId)
    {
        // TODO: Kiểm tra quyền (Admin hoặc chính Owner) rồi mới xóa
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Business>> GetPendingListingsAsync()
    {
        // TODO: return await _businessRepository.GetPendingAsync();
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateListingStatusAsync(int businessId, string status)
    {
        // TODO: _businessRepository.UpdateStatusAsync(businessId, status);
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<HomestayCardViewModel>> GetFeaturedHomestaysAsync(int top = 3) // NamNS
    {
        //1. take raw data
        var business = await _businessRepository.GetFeaturedHomestaysAsync(top);
        //2. Map entitu -> ViewModel
        return business.Select(b => new HomestayCardViewModel
        {
            BusinessId = b.BusinessId,
            Name = b.Name,
            ThumbnailUrl = b.Image,
            Address = b.Address,
            AreaName = b.Area?.Name,                    //area maybe null
            PriceFrom = b.PricePerNight ?? 0,
            AverageRating = (double)(b.AvgRating ?? 0),
            ReviewCount = b.ReviewCount ?? 0
        });
    }

    public async Task<IEnumerable<RestaurantCardViewModel>> GetFeaturedRestaurantAsync(int top = 3)
    {
        var business = await _businessRepository.GetFeaturedRestaurantAsync(top);
        return business.Select(b => new RestaurantCardViewModel
        {
            BusinessId = b.BusinessId,
            Name = b.Name,
            ThumbnailUrl = b.Image,
            Address = b.Address,
            AreaName = b.Area?.Name,
            AverageRating = (double)(b.AvgRating ?? 0),
            ReviewCount = b.ReviewCount ?? 0,
            OpeningTime = b.OpeningHour?.ToString(@"HH:mm"),
            ClosingTime = b.ClosingHour?.ToString(@"HH:mm"),
        });
    }

}
