using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entitys;
using MoviesAPI.Helpers;

namespace MoviesAPI.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>()
            where TEntity : class
        {
            var entitys = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            var dtos = _mapper.Map<List<TDTO>>(entitys);

            return dtos;
        }


        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(int id)
            where TEntity : class, IId
        {
            var entity = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();

            return _mapper.Map<TDTO>(entity);
        }


        protected async Task<ActionResult> Post<TEntity, TCreatorDTO, TReadDTO>(TCreatorDTO creatorDTO, string routeName)
            where TEntity : class, IId
        {
            var entity = _mapper.Map<TEntity>(creatorDTO);

            _context.Add(entity);
            await _context.SaveChangesAsync();

            var entityDTO = _mapper.Map<TReadDTO>(entity);

            return new CreatedAtRouteResult(routeName, new { id = entity.Id }, entityDTO);
        }

        protected async Task<ActionResult> Put<TEntity, TCreatorDTO>(int id, TCreatorDTO creatorDTO)
            where TEntity : class, IId
        {
            var exist = await _context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id);
            if (!exist) return NotFound("The specified id was not found");

            var entity = _mapper.Map<TEntity>(creatorDTO);
            entity.Id = id;

            _context.Update(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected async Task<ActionResult> Delete<TEntity>(int id)
            where TEntity : class, IId, new()
        {
            var exist = await _context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id);
            if (!exist) return NotFound("The specified id was not found");

            _context.Remove(new TEntity() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(PaginationDTO paginationDTO)
            where TEntity : class
        {
            var queryable = _context.Acthors.AsQueryable();
            await HttpContext.InsertPagingParameters(queryable, paginationDTO.NumberEntriesPerPage);

            var entityList = await queryable.Paginate(paginationDTO).ToListAsync();
            var entityListToShow = _mapper.Map<List<TDTO>>(entityList);

            return entityListToShow;
        }


        protected async Task<ActionResult> Patch<TEntity, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument)
            where TDTO : class
            where TEntity : class, IId
        {
            if (patchDocument == null) return BadRequest();

            var entityDB = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entityDB == null) return NotFound("The specified id was not found");

            var entityDTO = _mapper.Map<TDTO>(entityDB);
            patchDocument.ApplyTo(entityDTO, ModelState);

            var isValid = TryValidateModel(entityDTO);
            if (!isValid) return BadRequest(ModelState);

            _mapper.Map(entityDTO, entityDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
