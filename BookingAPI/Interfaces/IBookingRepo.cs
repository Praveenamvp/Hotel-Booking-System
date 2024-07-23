using BookingAPI.Models;

namespace BookingAPI.Interfaces
{
    public interface IBookingRepo<K, T>
    {
        T Get(int bookingId);
        ICollection<T> GetAll();
        T Add(Booking booking); 
        T Delete(int bookingId);
        T Update(Booking booking);



    }
}
