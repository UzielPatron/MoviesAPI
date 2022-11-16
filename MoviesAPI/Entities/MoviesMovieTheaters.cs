using MoviesAPI.Entitys;

namespace MoviesAPI.Entities
{
    public class MoviesMovieTheaters
    {
        public int MovieId { get; set; }
        public int MovieTheaterId { get; set; }
        public Movie Movie { get; set; }
        public MovieTheater MovieTheater { get; set; }

    }
}
