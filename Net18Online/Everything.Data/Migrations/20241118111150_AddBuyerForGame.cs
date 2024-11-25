using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBuyerForGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_BuyerId",
                table: "Games",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_BuyerId",
                table: "Games",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_BuyerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_BuyerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Games");
        }
    }
}
