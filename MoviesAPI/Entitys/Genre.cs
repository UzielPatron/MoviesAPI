using System.ComponentModel.DataAnnotations;
using MoviesAPI.Entitys.Others;
using MoviesAPI.Entitys.RelationEntitys;

namespace MoviesAPI.Entitys
{
    public class Genre : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public List<MoviesGenres> MoviesGenres { get; set; }
    }
}
