using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MoviesAPI.DTOs.Acthor;
using MoviesAPI.DTOs.Genre;
using MoviesAPI.DTOs.Movie;
using MoviesAPI.DTOs.MovieTheater;
using MoviesAPI.DTOs.User;
using MoviesAPI.Entities;
using MoviesAPI.Entitys;
using MoviesAPI.Entitys.RelationEntitys;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            // genres
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreatorDTO, Genre>();

            //actors
            CreateMap<Acthor, ActhorDTO>().ReverseMap();
            CreateMap<ActhorCreatorDTO, Acthor>()
                .ForMember(x => x.Photo, options => options.Ignore());
            CreateMap<ActhorPatchDTO, Acthor>().ReverseMap();

            // movies
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieCreatorDTO, Movie>()
                .ForMember(x => x.PosterImg, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MoviesActhors, options => options.MapFrom(MapMoviesActhors));
            CreateMap<MoviePatchDTO, Movie>().ReverseMap();
            CreateMap<Movie, MovieDetailDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.Acthors, options => options.MapFrom(MapMoviesActhors));

            // movieTheaters
            CreateMap<MovieTheater, MovieTheaterDTO>()
                    .ForMember(x => x.Latitude, x => x.MapFrom(y => y.Ubication.Y))
                    .ForMember(x => x.Longitude, x => x.MapFrom(y => y.Ubication.X));
            CreateMap<MovieTheaterDTO, MovieTheater>()
                .ForMember(
                x => x.Ubication,
                x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));
            CreateMap<MovieTheaterCreatorDTO, MovieTheater>()
                .ForMember(
                x => x.Ubication,
                x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));


            // users
            CreateMap<IdentityUser, UserDTO>();
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

        private List<GenreDTO> MapMoviesGenres(Movie movie, MovieDetailDTO movieDetailDTO)
        {
            var result = new List<GenreDTO>();
            if (movie.MoviesGenres == null) return result;

            foreach(var genreMovie in movie.MoviesGenres)
            {
                result.Add(new GenreDTO() { Id = genreMovie.GenreId, Name = genreMovie.Genre.Name });
            }

            return result;
        }

        private List<ActhorMovieDetailDTO> MapMoviesActhors(Movie movie, MovieDetailDTO movieDetailDTO)
        {
            var result = new List<ActhorMovieDetailDTO>();
            if (movie.MoviesActhors == null) return result;

            foreach(var acthorMovie in movie.MoviesActhors)
            {
                result.Add(new ActhorMovieDetailDTO()
                {
                    ActhorId = acthorMovie.ActhorId,
                    Character = acthorMovie.Character,
                    NameActhor = acthorMovie.Acthor.Name
                });
            }

            return result;
        }
    }
}
