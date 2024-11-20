using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorForMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatorId",
                table: "Movies",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_CreatorId",
                table: "Movies",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_CreatorId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CreatorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Movies");
        }
    }
}
