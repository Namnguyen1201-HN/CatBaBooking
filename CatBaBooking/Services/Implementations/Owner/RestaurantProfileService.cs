using CatBaBooking.Repository.Interface;
using CatBaBooking.Service.Interface;
using CatBaBooking.ViewModels;

namespace CatBaBooking.Service;

public class RestaurantProfileService : IRestaurantProfileService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IAreaRepository _areaRepository;

    public RestaurantProfileService(IBusinessRepository businessRepository, IAreaRepository areaRepository)
    {
        _businessRepository = businessRepository;
        _areaRepository = areaRepository;
    }

    public RestaurantProfileViewModel? GetProfile(int ownerId)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return null;
        }

        return new RestaurantProfileViewModel
        {
            BusinessId = business.BusinessId,
            Name = business.Name,
            Address = business.Address,
            Description = business.Description,
            Image = business.Image,
            AreaId = business.AreaId,
            OpeningHour = business.OpeningHour,
            ClosingHour = business.ClosingHour,
            AllAreas = _areaRepository.GetAll()
                .Select(a => new AreaOptionViewModel { AreaId = a.AreaId, Name = a.Name })
                .ToList()
        };
    }

    public (bool Success, string Message) UpdateProfile(int ownerId, RestaurantProfileViewModel model)
    {
        var business = _businessRepository.GetByOwnerId(ownerId);
        if (business == null || business.Type != "restaurant")
        {
            return (false, "Không tìm thấy nhà hàng của bạn.");
        }

        if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Address))
        {
            return (false, "Tên nhà hàng và địa chỉ không được để trống.");
        }

        business.Name = model.Name.Trim();
        business.Address = model.Address.Trim();
        business.Description = model.Description?.Trim() ?? string.Empty;
        business.Image = string.IsNullOrWhiteSpace(model.Image) ? null : model.Image.Trim();
        business.AreaId = model.AreaId;
        business.OpeningHour = model.OpeningHour;
        business.ClosingHour = model.ClosingHour;

        _businessRepository.Update(business);

        return (true, "Cập nhật thông tin nhà hàng thành công.");
    }
}