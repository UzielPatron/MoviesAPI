using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class MovieCreatorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
        [FileWeightValidation(maxWeightInMB: 4)]
        [FileTypeValidation(FileTypeGroup.Image)]
        public IFormFile PosterImg { get; set; }
    }
}
