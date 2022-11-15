using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class MoviePatchDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
