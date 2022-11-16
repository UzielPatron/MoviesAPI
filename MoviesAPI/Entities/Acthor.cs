using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entitys
{
    public class Acthor : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Photo { get; set; }
        public List<MoviesActhors> MoviesActhors { get; set; }
    }
}
