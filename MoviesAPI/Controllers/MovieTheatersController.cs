using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs.MovieTheater;
using MoviesAPI.Entities;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/theaters")]
    public class MovieTheatersController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly GeometryFactory _geometryFactory;

        public MovieTheatersController(
            ApplicationDbContext context,
            IMapper mapper,
            GeometryFactory geometryFactory)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _geometryFactory = geometryFactory;
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

        [HttpGet("nearby")]
        public async Task<ActionResult<List<NearbyMovieTheaterDTO>>> Get([FromQuery] NearbyMovieTheaterFilterDTO filter)
        {
            var userUbication = _geometryFactory.CreatePoint(new Coordinate(filter.Logitude, filter.Latitude));

            var movieTheaters = await _context.MovieTheaters
                .OrderBy(x => x.Ubication.Distance(userUbication))
                .Where(x => x.Ubication.IsWithinDistance(userUbication, filter.DistanceInKm * 1000))
                .Select(x => new NearbyMovieTheaterDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Latitude = x.Ubication.Y,
                    Longitude = x.Ubication.X,
                    DistanceInMt = Math.Round(x.Ubication.Distance(userUbication))
                })
                .ToListAsync();

            return movieTheaters;
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
