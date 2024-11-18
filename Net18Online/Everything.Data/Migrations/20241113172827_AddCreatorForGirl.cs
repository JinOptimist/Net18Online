using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorForGirl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Girls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Girls_CreatorId",
                table: "Girls",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Girls_Users_CreatorId",
                table: "Girls",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Girls_Users_CreatorId",
                table: "Girls");

            migrationBuilder.DropIndex(
                name: "IX_Girls_CreatorId",
                table: "Girls");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Girls");
        }
    }
}
