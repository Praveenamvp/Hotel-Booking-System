using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models.DTO
{
    public class UserDTO
    {
        public int? UserId { get; set; }

        [MinLength(3, ErrorMessage = " Name should be minimum 3 chars long")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
        public string? UserType { get; set; }
        public string? Token { get; set; }
    }
}
