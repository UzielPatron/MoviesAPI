using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.entities;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetAllGenres()
        {
            var genresList = await _context.Genres.ToListAsync();
            var genresListToShow = _mapper.Map<List<GenreDTO>>(genresList);

            return genresListToShow;
        }

        [HttpGet("{id:int}", Name = "getGenreById")]
        public async Task<ActionResult<GenreDTO>> GetGenreById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
            if (genre == null) return NotFound("No se encontró ningún género con el Id especificado");

            var genreToShow = _mapper.Map<GenreDTO>(genre);

            return genreToShow;
        }

        [HttpPost]
        public async Task<ActionResult> PostGenre([FromBody] GenreCreatorDTO genreCreatorDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreatorDTO);
            
            _context.Add(genre);
            await _context.SaveChangesAsync();

            var genreToShow = _mapper.Map<GenreDTO>(genre);

            return new CreatedAtRouteResult("getGenreById", new { id = genreToShow.Id }, genreToShow);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutGenre(int id, [FromBody] GenreCreatorDTO genreCreatorDTO)
        {
            var exist = await _context.Genres.AnyAsync(genre => genre.Id == id);
            if (!exist) return NotFound("No se encontró nigpun género con el Id especificado");

            var genre = _mapper.Map<Genre>(genreCreatorDTO);
            genre.Id = id;

            _context.Update(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var exist = await _context.Genres.AnyAsync(genre => genre.Id == id);
            if (!exist) return NotFound("No se encontró ningún género con el Id especificado");

            _context.Remove(new Genre { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
