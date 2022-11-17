using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.Acthor
{
    public class ActhorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Photo { get; set; }
    }
}
