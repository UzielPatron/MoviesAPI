using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/theaters")]
    public class MovieTheatersController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieTheatersController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get()
        {
            return await Get<MovieTheater, MovieTheaterDTO>();
        }

        [HttpGet("{id:int}", Name = "getMovieTheater")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(int id)
        {
            return await Get<MovieTheater, MovieTheaterDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieTheaterCreatorDTO movieTheaterCreatorDTO)
        {
            string route = "getMovieTheater";
            return await Post<MovieTheater, MovieTheaterCreatorDTO, MovieTheaterDTO>(movieTheaterCreatorDTO, route);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] MovieTheaterCreatorDTO movieTheaterCreatorDTO)
        {
            return await Put<MovieTheater, MovieTheaterCreatorDTO>(id, movieTheaterCreatorDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<MovieTheater>(id);
        }
    }
}
