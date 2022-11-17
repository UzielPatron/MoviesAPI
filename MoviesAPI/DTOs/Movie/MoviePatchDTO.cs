using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.Movie
{
    public class MoviePatchDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
