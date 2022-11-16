using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class MovieTheaterCreatorDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}
