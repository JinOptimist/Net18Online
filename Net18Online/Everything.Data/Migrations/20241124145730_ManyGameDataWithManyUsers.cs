using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyGameDataWithManyUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "GameDataUserData",
                columns: table => new
                {
                    BuyersId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDataUserData", x => new { x.BuyersId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameDataUserData_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDataUserData_Users_BuyersId",
                        column: x => x.BuyersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameDataUserData_GamesId",
                table: "GameDataUserData",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDataUserData");

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
    }
}
