using HotelAPI.Interfaces;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace HotelAPI.Services
{
    public class HotelRepo : IHotelRepo<int, Hotel>
    {
        private readonly HotelContext _context;

        public HotelRepo(HotelContext hotelContext) {
        _context= hotelContext;
        }
        public ICollection<Hotel> GetAll()
        {
            ICollection<Hotel> hotels = _context.Hotels.ToList();

            if (hotels != null)
            {
                return hotels;
            }
            return null;
        }
        

        public Hotel Get(int hotelId)
        {
            try
            {
                var hotels = _context.Hotels.FirstOrDefault(u => u.HotelId == hotelId);
                return hotels;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

       public Hotel Update(Hotel hotel)
        {
            try
            {
                var hotelData = _context.Hotels.FirstOrDefault(u => u.HotelName == hotel.HotelName);
                if (hotelData != null)
                {
                    hotelData.HotelName = hotel.HotelName;
                    hotelData.Address = hotel.Address;
                    hotelData.PhoneNumber = hotel.PhoneNumber;
                    hotelData.Location = hotel.Location;
                    hotelData.TotalRooms=hotel.TotalRooms;
                    hotelData.Ratings = hotel.Ratings;
                    hotelData.Amenities=hotel.Amenities;
                    _context.SaveChanges();
                    return hotelData;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

       

        public Hotel Delete(int hotelId)
        {

            try
            {
                Hotel hotel = Get(hotelId);
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
                return hotel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);


            }
            return null;
        }
        public Hotel Add(Hotel hotel)
        {

            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                return hotel;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(hotel);
            }
            return null;

        }


    }
}
