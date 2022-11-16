using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;
using System.Runtime.CompilerServices;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string container = "movies";

        public MoviesController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<MoviesIndexDTO>> GetMovies()
        {
            var top = 5;
            var today = DateTime.Today;

            var upcomingReleases = await _context.Movies
                .Where(x => x.ReleaseDate > today)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inTheaters = await _context.Movies
                .Where(x => x.InTheater)
                .Take(top)
                .ToListAsync();

            var result = new MoviesIndexDTO();
            result.UpcomingReleases = _mapper.Map<List<MovieDTO>>(upcomingReleases);
            result.InTheater = _mapper.Map<List<MovieDTO>>(inTheaters);

            return result;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> GetFilterMovies([FromQuery] FilterMovieDTO filterMovieDTO)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterMovieDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(filterMovieDTO.Title));
            }

            if (filterMovieDTO.InTheater)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InTheater);
            }

            if (filterMovieDTO.upcomingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
            }

            if(filterMovieDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable
                    .Where(x => x.MoviesGenres.Select(y => y.GenreId).Contains(filterMovieDTO.GenreId));
            }

            await HttpContext.InsertPagingParameters(moviesQueryable, filterMovieDTO.numberEntriesPerPage);

            var movies = await moviesQueryable.Paginate(filterMovieDTO.Pagination).ToListAsync();

            var moviesToShow = _mapper.Map<List<MovieDTO>>(movies);
            return moviesToShow;
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public async Task<ActionResult<MovieDetailDTO>> GetMovieById(int id)
        {
            Movie movie = await _context.Movies
                .Include(x => x.MoviesActhors).ThenInclude(x => x.Acthor)
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(movie => movie.Id == id);
            if (movie == null) return NotFound();

            movie.MoviesActhors = movie.MoviesActhors.OrderBy(x => x.Order).ToList();

            MovieDetailDTO movieToShow = _mapper.Map<MovieDetailDTO>(movie);
            return movieToShow;
        }

        [HttpPost]
        public async Task<ActionResult> PostMovie([FromForm] MovieCreatorDTO movieCreatorDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreatorDTO);

            if (movieCreatorDTO.PosterImg != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreatorDTO.PosterImg.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreatorDTO.PosterImg.FileName);
                    var contentType = movieCreatorDTO.PosterImg.ContentType;
                    movie.PosterImg = await _fileStorage.SaveFile(content, extension, container, contentType);
                }
            }

            AssignActhorsOrder(movie);
            _context.Add(movie);
            await _context.SaveChangesAsync();

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            return new CreatedAtRouteResult("getMovie", new { id = movie.Id }, movieDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutMovie(int id, [FromForm] MovieCreatorDTO movieCreatorDTO)
        {
            var movieDB = await _context.Movies
                .Include(x => x.MoviesActhors)
                .Include(x => x.MoviesGenres)
                .FirstOrDefaultAsync(movie => movie.Id == id);
            if (movieDB == null) return NotFound();

            movieDB = _mapper.Map(movieCreatorDTO, movieDB);

            if(movieCreatorDTO.PosterImg != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreatorDTO.PosterImg.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreatorDTO.PosterImg.FileName);
                    var route = movieDB.PosterImg;
                    var contentType = movieCreatorDTO.PosterImg.ContentType;

                    movieDB.PosterImg = await _fileStorage.EditFile(content, extension, container, route, contentType);
                }
            }

            AssignActhorsOrder(movieDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchMovie(int id, [FromBody] JsonPatchDocument<MoviePatchDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var movieDB = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
            if (movieDB == null) return NotFound();

            var moviePatchDTO = _mapper.Map<MoviePatchDTO>(movieDB);
            patchDocument.ApplyTo(moviePatchDTO, ModelState);

            var isValid = TryValidateModel(moviePatchDTO);
            if (!isValid) return BadRequest();

            _mapper.Map(moviePatchDTO, movieDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var exist = await _context.Movies.AnyAsync(movie => movie.Id == id);
            if (!exist) return NotFound();

            _context.Remove(new Movie() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private void AssignActhorsOrder(Movie movie)
        {
            if(movie.MoviesActhors != null)
            {
                for(int i = 0; i < movie.MoviesActhors.Count; i++)
                {
                    movie.MoviesActhors[i].Order = i;
                }
            }
        }
    }
}
