using MoviesAPI.DTOs.Genre;

namespace MoviesAPI.DTOs.Movie
{
    public class MovieDetailDTO : MovieDTO
    {
        public List<GenreDTO> Genres { get; set; }
        public List<ActhorMovieDetailDTO> Acthors { get; set; }
    }
}
