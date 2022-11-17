using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.User
{
    public class UserInfoDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
