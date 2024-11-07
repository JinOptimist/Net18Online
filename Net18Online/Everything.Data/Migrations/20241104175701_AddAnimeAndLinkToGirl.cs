using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeAndLinkToGirl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MangaId",
                table: "Girls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Girls_MangaId",
                table: "Girls",
                column: "MangaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Girls_Mangas_MangaId",
                table: "Girls",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Girls_Mangas_MangaId",
                table: "Girls");

            migrationBuilder.DropTable(
                name: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Girls_MangaId",
                table: "Girls");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Girls");
        }
    }
}
