using RoomAPI.Models;

namespace RoomAPI.Interfaces
{
    public interface IRoomRepo<K,G,T>
    {
        T Get(int roomId, int hotelId);
        ICollection<T> GetAll();
        T Add(Room room);
        T Update(Room hotel);
        T Delete(int roomlId, int hotelId);

    }
}
