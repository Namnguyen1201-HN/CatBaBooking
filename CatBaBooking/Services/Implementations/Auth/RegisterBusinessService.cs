using CatBaBooking.Models;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Service.Interface.Auth;

namespace CatBaBooking.Service;

public class RegisterBusinessService : IRegisterBusinessService
{
    private readonly IBusinessRepository _businessRepository;

    public RegisterBusinessService(IBusinessRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public bool RegisterBusiness(string name, string address, int ownerId, string type, string description)
    {
        bool checkOwner = _businessRepository.AnyBusinessByOwnerId(ownerId);
        if (checkOwner) return false;

        var newBusinesses = new Business(
            0,
            ownerId,
            name,
            type,
            address,
            description,
            "pending",
            DateTime.Now,
            DateTime.Now);

        return _businessRepository.AddBusiness(newBusinesses);
    }
}
