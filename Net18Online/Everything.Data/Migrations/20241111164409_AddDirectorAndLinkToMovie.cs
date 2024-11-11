using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorAndLinkToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmDirectorId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FilmDirectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmDirectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FilmDirectorId",
                table: "Movies",
                column: "FilmDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_FilmDirectors_FilmDirectorId",
                table: "Movies",
                column: "FilmDirectorId",
                principalTable: "FilmDirectors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_FilmDirectors_FilmDirectorId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "FilmDirectors");

            migrationBuilder.DropIndex(
                name: "IX_Movies_FilmDirectorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "FilmDirectorId",
                table: "Movies");
        }
    }
}
