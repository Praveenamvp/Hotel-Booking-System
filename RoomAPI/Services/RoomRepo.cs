using RoomAPI.Interfaces;
using RoomAPI.Models;
using System.Diagnostics;

namespace RoomAPI.Services
{
    public class RoomRepo:IRoomRepo<int,int,Room>
    {
        private RoomContext _context;

        public RoomRepo(RoomContext roomContext) {
        _context=roomContext;
        }
        public Room Add(Room room)
        {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return room;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(room);
            }
            return null;
        }
        public ICollection<Room> GetAll()
        {
            ICollection<Room> rooms = _context.Rooms.ToList();

            if (rooms != null)
            {
                return rooms;
            }
            return null;
        }


        public Room Get(int roomNumber,int hotelId)
        {
            try
            {
                Room roomDetails = _context.Rooms.FirstOrDefault(u => u.RoomNumber == roomNumber&& u.HotelId==hotelId);
                return roomDetails;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                

            }
            return null;
        }
        public Room Update(Room room)
        {
            try
            {
                Room roomDetails = _context.Rooms.FirstOrDefault(u => u.RoomNumber == room.RoomNumber && u.HotelId == room.HotelId);
                if (roomDetails != null)
                {
                    roomDetails.RoomType = room.RoomType;
                    roomDetails.Availability = room.Availability;
                    roomDetails.RoomCapacity = room.RoomCapacity;
                    roomDetails.RoomPrice = room.RoomPrice;
                    
                    _context.SaveChanges();
                    return roomDetails;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
            }

            return null;
        }
        public Room Delete(int rooNumber, int hotelId)
        {

            try
            {
                Room room = Get(rooNumber, hotelId);
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                return room;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);


            }
            return null;
        }

    }
}
