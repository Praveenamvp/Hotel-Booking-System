using BookingAPI.Interfaces;
using BookingAPI.Models;

namespace BookingAPI.Services
{
    public class BookingService
    {
        private readonly IBookingRepo<int, Booking> _repo;

        public BookingService(IBookingRepo<int,Booking> bookingRepo) {
            _repo=bookingRepo;
        }
        public Booking CheckStatusOfRoom(Booking booking)
        {
            ICollection<Booking> bookingDetails=_repo.GetAll().Where(u=>u.HotelId==booking.HotelId).ToList();
            if(bookingDetails.Count>0)
            {
                var bookings = bookingDetails.FirstOrDefault(u => u.RoomId == booking.RoomId);
                if(bookings!=null)
                {
                    return bookings;
                }

            }
            return null;

        }
    }
}
