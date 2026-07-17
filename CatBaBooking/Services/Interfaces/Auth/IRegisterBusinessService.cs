namespace CatBaBooking.Service.Interface.Auth;

public interface IRegisterBusinessService
{
    bool RegisterBusiness(string name, string address, int ownerId, string type, string description);
}
