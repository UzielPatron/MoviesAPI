using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entitys;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : CustomBaseController
    {
        public GenresController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetAllGenres()
        {
            return await Get<Genre, GenreDTO>(); 
        }

        [HttpGet("{id:int}", Name = "getGenreById")]
        public async Task<ActionResult<GenreDTO>> GetGenreById(int id)
        {
            return await Get<Genre, GenreDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostGenre([FromBody] GenreCreatorDTO genreCreatorDTO)
        {
            return await Post<Genre, GenreCreatorDTO, GenreDTO >(genreCreatorDTO, "getGenreById");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutGenre(int id, [FromBody] GenreCreatorDTO genreCreatorDTO)
        {
            return await Put<Genre, GenreCreatorDTO>(id, genreCreatorDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            return await Delete<Genre>(id);
        }
    }
}
