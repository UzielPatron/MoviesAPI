using AutoMapper;
using MoviesAPI.DTOs;
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

            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieCreatorDTO, Movie>()
                .ForMember(x => x.PosterImg, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MoviesActhors, options => options.MapFrom(MapMoviesActhors));
            CreateMap<MoviePatchDTO, Movie>().ReverseMap();
        }

        
        private List<MoviesGenres> MapMoviesGenres(MovieCreatorDTO movieCreatorDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();
            if (movieCreatorDTO.GenresIds == null) return result;

            foreach(var id in movieCreatorDTO.GenresIds)
            {
                result.Add(new MoviesGenres() { GenreId = id });
            }

            return result;
        }

        private List<MoviesActhors> MapMoviesActhors(MovieCreatorDTO movieCreatorDTO, Movie movie)
        {
            var result = new List<MoviesActhors>();
            if (movieCreatorDTO.Acthors == null) return result;

            foreach(var acthor in movieCreatorDTO.Acthors)
            {
                result.Add(new MoviesActhors() { ActhorId = acthor.ActhorId, Character = acthor.Character });
            }

            return result;
        }
    }
}
