using MoviesAPI.DTOs;

namespace MoviesAPI.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                    .Skip((paginationDTO.Page - 1) * paginationDTO.NumberEntriesPerPage)
                    .Take(paginationDTO.NumberEntriesPerPage);
        }
    }
}
