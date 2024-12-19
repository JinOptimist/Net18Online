using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddlikesAndDislikeForGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameDataUserData1",
                columns: table => new
                {
                    GameWhichUsersLikeId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoLikedGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDataUserData1", x => new { x.GameWhichUsersLikeId, x.UsersWhoLikedGameId });
                    table.ForeignKey(
                        name: "FK_GameDataUserData1_Games_GameWhichUsersLikeId",
                        column: x => x.GameWhichUsersLikeId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDataUserData1_Users_UsersWhoLikedGameId",
                        column: x => x.UsersWhoLikedGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDataUserData2",
                columns: table => new
                {
                    GameWhichUsersDislikeId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoDislikedGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDataUserData2", x => new { x.GameWhichUsersDislikeId, x.UsersWhoDislikedGameId });
                    table.ForeignKey(
                        name: "FK_GameDataUserData2_Games_GameWhichUsersDislikeId",
                        column: x => x.GameWhichUsersDislikeId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDataUserData2_Users_UsersWhoDislikedGameId",
                        column: x => x.UsersWhoDislikedGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameDataUserData1_UsersWhoLikedGameId",
                table: "GameDataUserData1",
                column: "UsersWhoLikedGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDataUserData2_UsersWhoDislikedGameId",
                table: "GameDataUserData2",
                column: "UsersWhoDislikedGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDataUserData1");

            migrationBuilder.DropTable(
                name: "GameDataUserData2");
        }
    }
}
