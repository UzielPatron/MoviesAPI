using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;

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
        public async Task<ActionResult<List<MovieDTO>>> GetMovies()
        {
            List<Movie> movieList = await _context.Movies.ToListAsync();
            List<MovieDTO> movieListToShow = _mapper.Map<List<MovieDTO>>(movieList);

            return movieListToShow;
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
            if (movie == null) return NotFound();

            MovieDTO movieToShow = _mapper.Map<MovieDTO>(movie);
            return movieToShow;
        }

        [HttpPost]
        public async Task<ActionResult> PostMovie([FromForm] MovieCreatorDTO movieCreatorDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreatorDTO);
            
            if(movieCreatorDTO.PosterImg != null)
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

            _context.Add(movie);
            await _context.SaveChangesAsync();

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            return new CreatedAtRouteResult("getMovie", new { id = movie.Id }, movieDTO);
        }
    }
}
