using HotelAPI.Models;

namespace HotelAPI.Interfaces
{
    public interface IHotelRepo<K,T>
    {
        T Get(int hotelId);
        ICollection<T> GetAll();
        T Add(Hotel hotel);
        T Update(Hotel hotel);
        T Delete(int hotelId);
    }
}
