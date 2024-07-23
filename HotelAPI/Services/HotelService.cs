using HotelAPI.Interfaces;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelAPI.Services
{
    public class HotelService
    {
        private readonly IHotelRepo<int, Hotel> _repo;

        public HotelService(IHotelRepo<int,Hotel> repo) {
            _repo = repo;
    
        }
        public ICollection<Hotel> GetHotelsByLocation(string location)
        {
            ICollection<Hotel> hotels = _repo.GetAll().Where(p => p.Location.ToLower() ==location.ToLower()).ToList();
            if(hotels.Count > 0 )
            {
                return hotels;
            }
            return null;

        }
        public ICollection<Hotel> GetHotelsByAmenities(string amenities)
        {
            ICollection<Hotel> hotels = _repo.GetAll().ToList();
        
            hotels = hotels.Where(p => p.Amenities.ToLower().Contains(amenities.ToLower())).ToList();
            if (hotels.Count > 0)
            {
                return hotels;
            }
            return null;

        }
        public ICollection<Hotel> GetHotelDetailsByName(string hotelName) {
            ICollection<Hotel> hotels = _repo.GetAll().Where(p => p.HotelName.ToLower() == hotelName.ToLower()).ToList();
            if (hotels != null)
            {
                return hotels;
            }
            return null;
        }
    }
}
