using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.DTOs.Acthor;
using MoviesAPI.Entitys;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/acthors")]
    public class ActhorsController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string container = "acthors";

        public ActhorsController(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActhorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            return await Get<Acthor, ActhorDTO>(paginationDTO);
        }

        [HttpGet("{id:int}", Name = "getActhorById")]
        public async Task<ActionResult<ActhorDTO>> Get(int id)
        {
            return await Get<Acthor, ActhorDTO>(id);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActhorCreatorDTO acthorCreatorDTO)
        {
            Acthor newActhor = _mapper.Map<Acthor>(acthorCreatorDTO);
            
            if(acthorCreatorDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await acthorCreatorDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(acthorCreatorDTO.Photo.FileName);
                    var contentType = acthorCreatorDTO.Photo.ContentType;

                    newActhor.Photo = await _fileStorage.SaveFile(content, extension, container, contentType);
                }
            }

            _context.Add(newActhor);
            await _context.SaveChangesAsync();

            ActhorDTO newActhorToShow = _mapper.Map<ActhorDTO>(newActhor);

            return new CreatedAtRouteResult("getActhorById", new { id = newActhor.Id }, newActhorToShow);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActhorCreatorDTO acthorModified)
        {
            var acthorDB = await _context.Acthors.FirstOrDefaultAsync(acthor => acthor.Id == id);
            if (acthorDB == null) return NotFound();

            acthorDB = _mapper.Map(acthorModified, acthorDB); // toma los campos de acthorCreatorDTO y los mapea a acthorDB
                                                              // los campos que sean distintos serán actualizados
            
            if(acthorModified.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await acthorModified.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(acthorModified.Photo.FileName);
                    var contentType = acthorModified.Photo.ContentType;
                    var route = acthorDB.Photo;

                    acthorDB.Photo = await _fileStorage.EditFile(content, extension, container, route, contentType);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActhorPatchDTO> patchDocument)
        {
            return await Patch<Acthor, ActhorPatchDTO>(id, patchDocument);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Acthor>(id);
        }

    }
}
