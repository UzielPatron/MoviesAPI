using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

namespace MoviesAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActhors>()
                .HasKey(x => new { x.ActhorId, x.MovieId });

            modelBuilder.Entity<MoviesGenres>()
                .HasKey(x => new { x.MovieId, x.GenreId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Acthor> Acthors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesActhors> MoviesActhors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
    }
}
