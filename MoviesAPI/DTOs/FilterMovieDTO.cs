namespace MoviesAPI.DTOs
{
    public class FilterMovieDTO
    {
        public int Page { get; set; } = 1;
        public int numberEntriesPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get { return new PaginationDTO() { Page = Page, NumberEntriesPerPage = numberEntriesPerPage }; }
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheater { get; set; }
        public bool upcomingReleases { get; set; }
    }
}
