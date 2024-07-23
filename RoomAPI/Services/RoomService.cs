using RoomAPI.Interfaces;
using RoomAPI.Models;
using System.Linq;

namespace RoomAPI.Services
{
    public class RoomService
    {
        private readonly IRoomRepo<int,int, Room> _repo;

        public RoomService(IRoomRepo<int,int, Room> repo)
        {
            _repo = repo;
        }

        public ICollection<Room> RoomsByPrice(int price) {
            ICollection<Room> rooms = _repo.GetAll().Where(u=>u.RoomPrice==price).ToList(); 
            if(rooms.Count!=0)
            {
                return rooms;
            }
            return null;
        }
        public ICollection<Room> RoomsByPriceRange(int minPrice,int maxPrice)
        {
            ICollection<Room> rooms = _repo.GetAll().Where(p => p.RoomPrice >= minPrice && p.RoomPrice <= maxPrice).ToList();
            if (rooms.Count != 0)
            {
                return rooms;
            }
            return null;
        }
        public int  CountofAvailableRooms()
        {
            ICollection<Room> roomDetails = _repo.GetAll().Where(p => (p.Availability.ToLower()).Contains("Available".ToLower())).ToList();
            int rooms=roomDetails.Count;
            return rooms;
    
        }
         public int CountofAvailableRoomsByHotel(int hotelId)
        {
            ICollection<Room> roomDetails = _repo.GetAll().Where(p => p.Availability.ToLower().Contains("Available".ToLower()) && p.HotelId==hotelId).ToList();
            int rooms = roomDetails.Count;
            return rooms;

        }
        public ICollection<Room> AvailableRooms()
        {
            ICollection<Room> rooms = _repo.GetAll().Where(p => p.Availability.ToLower().Contains("Available".ToLower())).ToList();
            return rooms;
        }
        public ICollection<Room> AvailableRoomsByHotelId(int hotelID)
        {
            ICollection<Room> rooms = _repo.GetAll().Where(p => p.HotelId== hotelID).ToList();
            return rooms;
        }


    }
}
