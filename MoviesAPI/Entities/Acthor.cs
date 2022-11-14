using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class Acthor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Photo { get; set; }
    }
}
