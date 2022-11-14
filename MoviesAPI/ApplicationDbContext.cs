using Microsoft.EntityFrameworkCore;
using MoviesAPI.entities;
using MoviesAPI.Entities;

namespace MoviesAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Acthor> Acthors { get; set; }
    }
}
