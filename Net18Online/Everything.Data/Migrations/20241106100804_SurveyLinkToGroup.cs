using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class SurveyLinkToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdGroup",
                table: "Surveys",
                newName: "SurveyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SurveyGroupId",
                table: "Surveys",
                column: "SurveyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyGroups_SurveyGroupId",
                table: "Surveys",
                column: "SurveyGroupId",
                principalTable: "SurveyGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyGroups_SurveyGroupId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_SurveyGroupId",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "SurveyGroupId",
                table: "Surveys",
                newName: "IdGroup");
        }
    }
}
