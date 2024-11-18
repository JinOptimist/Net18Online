using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorUserFromSurveyGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "СreatorUserId",
                table: "SurveyGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyGroups_СreatorUserId",
                table: "SurveyGroups",
                column: "СreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyGroups_Users_СreatorUserId",
                table: "SurveyGroups",
                column: "СreatorUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyGroups_Users_СreatorUserId",
                table: "SurveyGroups");

            migrationBuilder.DropIndex(
                name: "IX_SurveyGroups_СreatorUserId",
                table: "SurveyGroups");

            migrationBuilder.DropColumn(
                name: "СreatorUserId",
                table: "SurveyGroups");
        }
    }
}
