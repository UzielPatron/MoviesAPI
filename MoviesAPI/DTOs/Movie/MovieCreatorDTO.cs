using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Helpers;
using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.Movie
{
    public class MovieCreatorDTO : MoviePatchDTO
    {
        [FileWeightValidation(maxWeightInMB: 4)]
        [FileTypeValidation(FileTypeGroup.Image)]
        public IFormFile PosterImg { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<ActhorMovieCreatorDTO>>))]
        public List<ActhorMovieCreatorDTO> Acthors { get; set; }
    }
}
