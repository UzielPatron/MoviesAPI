using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class ActhorPatchDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
