using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.entities;
using MoviesAPI.Entities;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreatorDTO, Genre>();

            CreateMap<Acthor, ActhorDTO>().ReverseMap();
            CreateMap<ActhorCreatorDTO, Acthor>()
                .ForMember(x => x.Photo, options => options.Ignore());
            CreateMap<ActhorPatchDTO, Acthor>().ReverseMap();
        }
    }
}
