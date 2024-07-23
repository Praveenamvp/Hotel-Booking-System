using Microsoft.EntityFrameworkCore;

namespace UserAPI.Models
{
    public class UserContext:DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
