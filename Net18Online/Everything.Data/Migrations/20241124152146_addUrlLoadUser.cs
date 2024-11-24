using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUrlLoadUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "LoadVolumeTestingMetrics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "LoadUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LoadVolumeTestingMetrics_AuthorId",
                table: "LoadVolumeTestingMetrics",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadVolumeTestingMetrics_Users_AuthorId",
                table: "LoadVolumeTestingMetrics",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadVolumeTestingMetrics_Users_AuthorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropIndex(
                name: "IX_LoadVolumeTestingMetrics_AuthorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "LoadUsers");
        }
    }
}
