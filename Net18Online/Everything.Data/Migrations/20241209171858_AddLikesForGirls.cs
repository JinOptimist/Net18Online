using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLikesForGirls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GirlDataUserData",
                columns: table => new
                {
                    GirlsWhichUsersLikeId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoLikeItId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirlDataUserData", x => new { x.GirlsWhichUsersLikeId, x.UsersWhoLikeItId });
                    table.ForeignKey(
                        name: "FK_GirlDataUserData_Girls_GirlsWhichUsersLikeId",
                        column: x => x.GirlsWhichUsersLikeId,
                        principalTable: "Girls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GirlDataUserData_Users_UsersWhoLikeItId",
                        column: x => x.UsersWhoLikeItId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GirlDataUserData_UsersWhoLikeItId",
                table: "GirlDataUserData",
                column: "UsersWhoLikeItId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GirlDataUserData");
        }
    }
}
