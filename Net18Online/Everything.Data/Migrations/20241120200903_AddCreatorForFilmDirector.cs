using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorForFilmDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "FilmDirectors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmDirectors_CreatorId",
                table: "FilmDirectors",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmDirectors_Users_CreatorId",
                table: "FilmDirectors",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDirectors_Users_CreatorId",
                table: "FilmDirectors");

            migrationBuilder.DropIndex(
                name: "IX_FilmDirectors_CreatorId",
                table: "FilmDirectors");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "FilmDirectors");
        }
    }
}
