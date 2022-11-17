﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using MoviesAPI.Entitys;
using MoviesAPI.Entitys.RelationEntitys;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Security.Claims;

namespace MoviesAPI
{
    public class ApplicationDbContext : IdentityDbContext
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

            modelBuilder.Entity<MoviesMovieTheaters>()
                .HasKey(x => new { x.MovieId, x.MovieTheaterId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            string roleAdminId = "9183d1a8-d4ba-4d58-b2f1-b4888cb8aad1";
            string adminUserId = "68904d84-ebed-441b-98ae-62584102183c";
            string username = "pedro@picapiedra.com";

            var roleAdmin = new IdentityRole()
            {
                Id = roleAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var userAdmin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
            };

            modelBuilder.Entity<IdentityUser>()
                .HasData(userAdmin);

            modelBuilder.Entity<IdentityRole>()
                .HasData(roleAdmin);

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasData(new IdentityUserClaim<string>()
                {
                    Id = 1,
                    ClaimType = ClaimTypes.Role,
                    UserId = adminUserId,
                    ClaimValue = "Admin"
                });

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            modelBuilder.Entity<MovieTheater>()
               .HasData(new List<MovieTheater>
               {
                    new MovieTheater
                    {
                        Id = 1,
                        Name = "Hoyts Rosario",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-32.914754, -60.684117))
                    },
                    new MovieTheater
                    {
                        Id = 2,
                        Name = "Showcase Imax Olivos",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-34.515025, -58.524678))
                    },
                    new MovieTheater
                    {
                        Id = 3,
                        Name = "Cinépolis Recoleta",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-34.587816, -58.393633))
                    },
                    new MovieTheater
                    {
                        Id = 4,
                        Name = "CineMark Palermo",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-34.583921, -58.409382))
                    },
                    new MovieTheater
                    {
                        Id = 5,
                        Name = "Cine Rio Gallegos",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-51.619709, -69.218053))
                    },
                    new MovieTheater
                    {
                        Id = 6,
                        Name = "Cinema 3D La Rioja",
                        Ubication = geometryFactory.CreatePoint(new Coordinate(-29.413288, -66.863795))
                    }
               });


            var adventure = new Genre() { Id = 1, Name = "Adventure" };
            var animation = new Genre() { Id = 2, Name = "Animation" };
            var suspense = new Genre() { Id = 3, Name = "Suspense" };
            var action = new Genre() { Id = 4, Name = "Action" };
            var superhero = new Genre() { Id = 5, Name = "Superhero" };
            var romance = new Genre() { Id = 6, Name = "Romance" };


            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>
                {
                    adventure, animation, suspense, action, superhero, romance
                });

            var jimCarrey = new Acthor() { Id = 1, Name = "Jim Carrey", DateBirth = new DateTime(1962, 01, 17) };
            var robertDowney = new Acthor() { Id = 2, Name = "Robert Downey Jr.", DateBirth = new DateTime(1965, 4, 4) };
            var chrisEvans = new Acthor() { Id = 3, Name = "Chris Evans", DateBirth = new DateTime(1981, 06, 13) };
            var tomHolland = new Acthor() { Id = 4, Name = "Tom Holland", DateBirth = new DateTime(1996, 05, 04) };
            var benAffleck = new Acthor() { Id = 5, Name = "Ben Affleck", DateBirth = new DateTime(1972, 08, 15) };
            var henryCavill = new Acthor() { Id = 6, Name = "Henry Cavill", DateBirth = new DateTime(1983, 05, 05) };
            var galGadot = new Acthor() { Id = 7, Name = "Gal Gadot", DateBirth = new DateTime(1985, 04, 30)};


            modelBuilder.Entity<Acthor>()
                .HasData(new List<Acthor>
                {
                    jimCarrey, robertDowney, chrisEvans, tomHolland, benAffleck, henryCavill, galGadot
                });

            var endgame = new Movie()
            {
                Id = 1,
                Title = "Avengers: Endgame",
                InTheater = true,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var iw = new Movie()
            {
                Id = 2,
                Title = "Avengers: Infinity Wars",
                InTheater = false,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var sonic = new Movie()
            {
                Id = 3,
                Title = "Sonic the Hedgehog",
                InTheater = false,
                ReleaseDate = new DateTime(2020, 02, 28)
            };
            var emma = new Movie()
            {
                Id = 4,
                Title = "Emma",
                InTheater = false,
                ReleaseDate = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Movie()
            {
                Id = 5,
                Title = "Wonder Woman 1984",
                InTheater = false,
                ReleaseDate = new DateTime(2020, 08, 14)
            };

            var batmanvsuperman = new Movie()
            {
                Id = 6,
                Title = "Batman vs Superman",
                InTheater = false,
                ReleaseDate = new DateTime(2016, 06, 05)
            };

            modelBuilder.Entity<Movie>()
                .HasData(new List<Movie>
                {
                    endgame, iw, sonic, emma, wonderwoman, batmanvsuperman
                });

            modelBuilder.Entity<MoviesGenres>().HasData(
                new List<MoviesGenres>()
                {
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = superhero.Id},
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = superhero.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = sonic.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = superhero.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = batmanvsuperman.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = batmanvsuperman.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = batmanvsuperman.Id, GenreId = superhero.Id},
                });

            modelBuilder.Entity<MoviesActhors>().HasData(
                new List<MoviesActhors>()
                {
                    new MoviesActhors(){MovieId = endgame.Id, ActhorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActhors(){MovieId = endgame.Id, ActhorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActhors(){MovieId = endgame.Id, ActhorId = tomHolland.Id, Character = "Spiderman", Order = 3},
                    new MoviesActhors(){MovieId = iw.Id, ActhorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActhors(){MovieId = iw.Id, ActhorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActhors(){MovieId = iw.Id, ActhorId = tomHolland.Id, Character = "Spiderman", Order = 3},
                    new MoviesActhors(){MovieId = sonic.Id, ActhorId = jimCarrey.Id, Character = "Dr. Ivo Robotnik", Order = 1},
                    new MoviesActhors(){MovieId = batmanvsuperman.Id, ActhorId = benAffleck.Id, Character = "Batman / Bruce Wayne", Order = 1},
                    new MoviesActhors(){MovieId = batmanvsuperman.Id, ActhorId = henryCavill.Id, Character = "Superman / Clark Kent", Order = 2},
                    new MoviesActhors(){MovieId = batmanvsuperman.Id, ActhorId = galGadot.Id, Character = "Wonder Woman / Diana Prince", Order = 3},
                    new MoviesActhors(){MovieId = wonderwoman.Id, ActhorId = galGadot.Id, Character = "Wonder Woman / Diana Prince", Order = 1}
                });
        }

        private object IdentityRole()
        {
            throw new NotImplementedException();
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Acthor> Acthors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<MoviesActhors> MoviesActhors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MoviesMovieTheaters> MoviesMovieTheaters { get; set; }
    }
}
