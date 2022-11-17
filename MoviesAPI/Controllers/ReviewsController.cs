using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.DTOs.Reviews;
using MoviesAPI.Entitys;
using System.Security.Claims;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId:int}/reviews")]
    public class ReviewsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int movieId, [FromQuery] PaginationDTO paginationDTO)
        {
            var existMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);
            if (!existMovie) return NotFound("The specified movie id is not found");

            var queryable = _context.Reviews.Include(x => x.User).AsQueryable();
            queryable = queryable.Where(x => x.MovieId == movieId);

            return await Get<Review, ReviewDTO>(paginationDTO, queryable);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int movieId, [FromBody] ReviewCreatorDTO reviewCreatorDTO)
        {
            var existMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);
            if (!existMovie) return NotFound("The specified id is not found");

            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var reviewExist = await _context.Reviews.AnyAsync(x => x.MovieId == movieId && x.UserId == userId);
            if (!reviewExist) return BadRequest("The user has already written a review of the movie");
        
            var review = _mapper.Map<Review>(reviewCreatorDTO);
            review.MovieId = movieId;
            review.UserId = userId;

            _context.Add(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int movieId, int reviewId, [FromBody] ReviewCreatorDTO reviewCreatorDTO)
        {
            var existMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);
            if (!existMovie) return NotFound("The specified movie id is not found");

            var reviewDB = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId && x.MovieId == movieId);
            if (reviewDB == null) return NotFound("The specified review id is not found");

            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (reviewDB.UserId != userId) return Forbid();

            reviewDB = _mapper.Map(reviewCreatorDTO, reviewDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int movieId, int reviewId)
        {
            var existMovie = await _context.Movies.AnyAsync(x => x.Id == movieId);
            if (!existMovie) return NotFound("The specified movie id is not found");

            var reviewDB = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId && x.MovieId == movieId);
            if (reviewDB == null) return NotFound("The specified review id is not found");

            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (reviewDB.UserId != userId) return Forbid();

            _context.Remove(reviewDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
