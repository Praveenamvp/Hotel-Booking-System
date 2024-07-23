using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [MinLength(2, ErrorMessage = " Hotel name should be minimum 3 chars long")]
        public string HotelName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; } 
      
        public string Location { get; set; }
        [Range(1, 300, ErrorMessage = "Invalid ratings range ")]


        public int TotalRooms { get; set; }
       [Range(1, 5, ErrorMessage = "Invalid ratings range ")]
        public int Ratings { get; set; }
        public string Amenities { get; set; }
    }
}
