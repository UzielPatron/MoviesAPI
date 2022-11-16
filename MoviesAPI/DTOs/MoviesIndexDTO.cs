namespace MoviesAPI.DTOs
{
    public class MoviesIndexDTO
    {
        public List<MovieDTO> UpcomingReleases { get; set; }
        public List<MovieDTO> InTheater { get; set; }
    }
}
