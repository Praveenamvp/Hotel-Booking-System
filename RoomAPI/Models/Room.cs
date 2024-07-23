using System.ComponentModel.DataAnnotations;

namespace RoomAPI.Models
{
    
        public class Room
        {
            
            public int HotelId { get; set; }
            [Key]
            public int RoomId { get; set; }
            [Required]
            public int RoomNumber { get; set; }
            public string RoomType { get; set; }
            [Required]
            public string Availability { get; set; }
             [Range(1, 15, ErrorMessage = "Invalid RoomCapacity  range ")]

             public int RoomCapacity { get; set; }
             [Range(1000, 25000, ErrorMessage = "Invalid Room Price  range ")]

             public int RoomPrice { get; set; }



        }

    }

