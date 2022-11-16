using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class movieTheatersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieTheters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTheters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesMovieTheaters",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieTheaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesMovieTheaters", x => new { x.MovieId, x.MovieTheaterId });
                    table.ForeignKey(
                        name: "FK_MoviesMovieTheaters_MovieTheters_MovieTheaterId",
                        column: x => x.MovieTheaterId,
                        principalTable: "MovieTheters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesMovieTheaters_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesMovieTheaters_MovieTheaterId",
                table: "MoviesMovieTheaters",
                column: "MovieTheaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesMovieTheaters");

            migrationBuilder.DropTable(
                name: "MovieTheters");
        }
    }
}
