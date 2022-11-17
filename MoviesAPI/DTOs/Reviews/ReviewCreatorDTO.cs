using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs.Reviews
{
    public class ReviewCreatorDTO
    {
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
    }
}
