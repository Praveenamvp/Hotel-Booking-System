using Microsoft.EntityFrameworkCore;

namespace RoomAPI.Models
{
    public class RoomContext:DbContext
    {
        public RoomContext() { }
        public RoomContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
    }
}
