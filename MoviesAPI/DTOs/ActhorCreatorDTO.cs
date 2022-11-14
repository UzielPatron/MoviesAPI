using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class ActhorCreatorDTO : ActhorPatchDTO
    {
        [FileWeightValidation(maxWeightInMB: 4)]
        [FileTypeValidation(fileTypeGroup: FileTypeGroup.Image)]
        public IFormFile Photo { get; set; }
    }
}
