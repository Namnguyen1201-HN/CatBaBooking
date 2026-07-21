using CatBaBooking.Models;

namespace CatBaBooking.Repository.Interface;

public interface IAreaRepository
{
    List<Area> GetAll();
}