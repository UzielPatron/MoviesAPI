using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Helpers
{
    public class ExistMovieAttribute : Attribute, IAsyncResourceFilter
    {
        private ApplicationDbContext _context;

        public ExistMovieAttribute(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var movieIdObject = context.HttpContext.Request.RouteValues["movieId"];
            if (movieIdObject == null) return;

            var movieId = int.Parse(movieIdObject.ToString());

            var existMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);
            if (!existMovie) context.Result = new NotFoundResult();
            else await next();
        }
    }
}
