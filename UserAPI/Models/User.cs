using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public int? UserId { get; set; }
        
        [MinLength(3, ErrorMessage = " name should be minimum 3 chars long")]
        public string UserName { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number")]

        public string PhoneNumber { get; set; }
        [Range(18, 100, ErrorMessage = "Invalid Age  ")]

        public int Age { get; set; }
        public string UserType { get; set; }
    }
}
