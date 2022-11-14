using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreCreatorDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
