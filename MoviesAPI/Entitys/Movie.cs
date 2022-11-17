using MoviesAPI.Entitys.Others;
using MoviesAPI.Entitys.RelationEntitys;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entitys
{
    public class Movie : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterImg { get; set; }
        public List<MoviesActhors> MoviesActhors { get; set; }
        public List<MoviesGenres> MoviesGenres { get; set; }
        public List<MoviesMovieTheaters> MoviesMoviesTheaters { get; set; }
    }
}
