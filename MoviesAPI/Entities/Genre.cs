﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
