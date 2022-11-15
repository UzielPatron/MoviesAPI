using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class MovieCreatorDTO : MoviePatchDTO
    {
        [FileWeightValidation(maxWeightInMB: 4)]
        [FileTypeValidation(FileTypeGroup.Image)]
        public IFormFile PosterImg { get; set; }
    }
}
