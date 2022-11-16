using MoviesAPI.Entitys;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class MovieTheater : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public List<MoviesMovieTheaters> MoviesMovieTheaters { get; set; }
    }
}
