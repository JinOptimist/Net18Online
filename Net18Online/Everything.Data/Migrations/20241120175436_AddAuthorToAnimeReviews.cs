using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorToAnimeReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AnimeReviews");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "AnimeReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeReviews_CreatorId",
                table: "AnimeReviews",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeReviews_Users_CreatorId",
                table: "AnimeReviews",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeReviews_Users_CreatorId",
                table: "AnimeReviews");

            migrationBuilder.DropIndex(
                name: "IX_AnimeReviews_CreatorId",
                table: "AnimeReviews");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AnimeReviews");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AnimeReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
