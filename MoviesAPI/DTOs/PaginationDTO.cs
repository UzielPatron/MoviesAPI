namespace MoviesAPI.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        private int numberEntriesPerPage = 10;
        private readonly int maxNumberEntriesPerPage = 50;

        public int NumberEntriesPerPage
        {
            get => numberEntriesPerPage;
            set
            {
                numberEntriesPerPage = (value > maxNumberEntriesPerPage) ? maxNumberEntriesPerPage : value;
            }
        }
    }
}
