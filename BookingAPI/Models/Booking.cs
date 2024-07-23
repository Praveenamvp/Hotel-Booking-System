using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [MinLength(3, ErrorMessage = " name should be minimum 3 chars long")]
        public string UserName { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut{ get; set; }
        [Required]

        public int TotalPrice { get; set; }



    }
}
