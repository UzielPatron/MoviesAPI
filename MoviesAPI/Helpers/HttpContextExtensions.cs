using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPagingParameters<T>(
            this HttpContext httpContext,
            IQueryable<T> queryable,
            int numberEntriesPerPage
            )
        {
            double quantityOfEntries = await queryable.CountAsync();
            double quantityPages = Math.Ceiling(quantityOfEntries / numberEntriesPerPage);

            httpContext.Response.Headers.Add("numberOfPages", quantityPages.ToString());
        }
    }
}
