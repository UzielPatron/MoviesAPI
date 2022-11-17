namespace MoviesAPI.DTOs.Movie
{
    public class FilterMovieDTO
    {
        public int Page { get; set; } = 1;
        public int NumberEntriesPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get { return new PaginationDTO() { Page = Page, NumberEntriesPerPage = NumberEntriesPerPage }; }
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheater { get; set; }
        public bool UpcomingReleases { get; set; }
        public string SortField { get; set; }
        public bool AscendingOrder { get; set; }
    }
}
