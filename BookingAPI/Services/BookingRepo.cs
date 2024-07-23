using BookingAPI.Interfaces;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingAPI.Services
{
    public class BookingRepo:IBookingRepo<int,Booking>
    {
        private readonly BookingContext _context;
        

        public BookingRepo(BookingContext bookingContext) {
            _context = bookingContext;
           
        }
        public ICollection<Booking> GetAll()
        {
            ICollection<Booking> bookings = _context.Bookings.ToList();

            if (bookings != null)
            {
                return bookings;
            }
            return null;
        }
        public Booking Get(int bookingId)
        {
            try
            {
                var bookings = _context.Bookings.FirstOrDefault(u => u.BookingId == bookingId);
                return bookings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public Booking Delete(int bookingId)
        {

            try
            {
                Booking booking = Get(bookingId);
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                return booking;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public Booking Add(Booking booking)
        {

            
                try
                {
                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                    return booking;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            
            return null;
     
        }
        public Booking Update(Booking booking)
        {
            try
            {
                Booking bookingDetails = _context.Bookings.FirstOrDefault(u => u.BookingId == booking.BookingId );
                if (bookingDetails != null)
                {
                    booking.RoomId = bookingDetails.RoomId;
                    booking.UserName = bookingDetails.UserName;
                    booking.CheckIn = bookingDetails.CheckIn;
                    booking.CheckOut = bookingDetails.CheckOut;
                    booking.TotalPrice  = bookingDetails.TotalPrice;
                    

                    _context.SaveChanges();
                    return bookingDetails;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }

            return null;
        }



    }
}
