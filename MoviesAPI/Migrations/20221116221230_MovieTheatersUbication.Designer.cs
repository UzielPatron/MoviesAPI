﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesAPI;
using NetTopologySuite.Geometries;

#nullable disable

namespace MoviesAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221116221230_MovieTheatersUbication")]
    partial class MovieTheatersUbication
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MoviesAPI.Entities.MovieTheater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<Point>("Ubication")
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("MovieTheaters");
                });

            modelBuilder.Entity("MoviesAPI.Entities.MoviesMovieTheaters", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("MovieTheaterId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "MovieTheaterId");

                    b.HasIndex("MovieTheaterId");

                    b.ToTable("MoviesMovieTheaters");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Acthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Acthors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateBirth = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jim Carrey"
                        },
                        new
                        {
                            Id = 2,
                            DateBirth = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 3,
                            DateBirth = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Chris Evans"
                        },
                        new
                        {
                            Id = 4,
                            DateBirth = new DateTime(1996, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Tom Holland"
                        },
                        new
                        {
                            Id = 5,
                            DateBirth = new DateTime(1972, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ben Affleck"
                        },
                        new
                        {
                            Id = 6,
                            DateBirth = new DateTime(1983, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Henry Cavill"
                        },
                        new
                        {
                            Id = 7,
                            DateBirth = new DateTime(1985, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Gal Gadot"
                        });
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Suspense"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Superhero"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Romance"
                        });
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("InTheater")
                        .HasColumnType("bit");

                    b.Property<string>("PosterImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InTheater = true,
                            ReleaseDate = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 2,
                            InTheater = false,
                            ReleaseDate = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 3,
                            InTheater = false,
                            ReleaseDate = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 4,
                            InTheater = false,
                            ReleaseDate = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Emma"
                        },
                        new
                        {
                            Id = 5,
                            InTheater = false,
                            ReleaseDate = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Wonder Woman 1984"
                        },
                        new
                        {
                            Id = 6,
                            InTheater = false,
                            ReleaseDate = new DateTime(2016, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Batman vs Superman"
                        });
                });

            modelBuilder.Entity("MoviesAPI.Entitys.MoviesActhors", b =>
                {
                    b.Property<int>("ActhorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("ActhorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesActhors");

                    b.HasData(
                        new
                        {
                            ActhorId = 2,
                            MovieId = 1,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            ActhorId = 3,
                            MovieId = 1,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            ActhorId = 4,
                            MovieId = 1,
                            Character = "Spiderman",
                            Order = 3
                        },
                        new
                        {
                            ActhorId = 2,
                            MovieId = 2,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            ActhorId = 3,
                            MovieId = 2,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            ActhorId = 4,
                            MovieId = 2,
                            Character = "Spiderman",
                            Order = 3
                        },
                        new
                        {
                            ActhorId = 1,
                            MovieId = 3,
                            Character = "Dr. Ivo Robotnik",
                            Order = 1
                        },
                        new
                        {
                            ActhorId = 5,
                            MovieId = 6,
                            Character = "Batman / Bruce Wayne",
                            Order = 1
                        },
                        new
                        {
                            ActhorId = 6,
                            MovieId = 6,
                            Character = "Superman / Clark Kent",
                            Order = 2
                        },
                        new
                        {
                            ActhorId = 7,
                            MovieId = 6,
                            Character = "Wonder Woman / Diana Prince",
                            Order = 3
                        },
                        new
                        {
                            ActhorId = 7,
                            MovieId = 5,
                            Character = "Wonder Woman / Diana Prince",
                            Order = 1
                        });
                });

            modelBuilder.Entity("MoviesAPI.Entitys.MoviesGenres", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MoviesGenres");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = 1,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 1,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 1,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 3,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 4,
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = 4,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 6,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 6,
                            GenreId = 3
                        },
                        new
                        {
                            MovieId = 6,
                            GenreId = 5
                        });
                });

            modelBuilder.Entity("MoviesAPI.Entities.MoviesMovieTheaters", b =>
                {
                    b.HasOne("MoviesAPI.Entitys.Movie", "Movie")
                        .WithMany("MoviesMoviesTheaters")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesAPI.Entities.MovieTheater", "MovieTheater")
                        .WithMany("MoviesMovieTheaters")
                        .HasForeignKey("MovieTheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("MovieTheater");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.MoviesActhors", b =>
                {
                    b.HasOne("MoviesAPI.Entitys.Acthor", "Acthor")
                        .WithMany("MoviesActhors")
                        .HasForeignKey("ActhorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesAPI.Entitys.Movie", "Movie")
                        .WithMany("MoviesActhors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acthor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.MoviesGenres", b =>
                {
                    b.HasOne("MoviesAPI.Entitys.Genre", "Genre")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesAPI.Entitys.Movie", "Movie")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesAPI.Entities.MovieTheater", b =>
                {
                    b.Navigation("MoviesMovieTheaters");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Acthor", b =>
                {
                    b.Navigation("MoviesActhors");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Genre", b =>
                {
                    b.Navigation("MoviesGenres");
                });

            modelBuilder.Entity("MoviesAPI.Entitys.Movie", b =>
                {
                    b.Navigation("MoviesActhors");

                    b.Navigation("MoviesGenres");

                    b.Navigation("MoviesMoviesTheaters");
                });
#pragma warning restore 612, 618
        }
    }
}
