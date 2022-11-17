using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.Movie
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterImg { get; set; }
    }
}
