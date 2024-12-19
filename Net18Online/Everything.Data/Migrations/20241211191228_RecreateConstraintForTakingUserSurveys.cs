using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecreateConstraintForTakingUserSurveys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakingUserSurveys_Surveys_SurveyId",
                table: "TakingUserSurveys");

            migrationBuilder.DropForeignKey(
                name: "FK_TakingUserSurveys_Users_UserId",
                table: "TakingUserSurveys");

            migrationBuilder.AddForeignKey(
                name: "FK_TakingUserSurveys_Surveys_SurveyId",
                table: "TakingUserSurveys",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TakingUserSurveys_Users_UserId",
                table: "TakingUserSurveys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakingUserSurveys_Surveys_SurveyId",
                table: "TakingUserSurveys");

            migrationBuilder.DropForeignKey(
                name: "FK_TakingUserSurveys_Users_UserId",
                table: "TakingUserSurveys");

            migrationBuilder.AddForeignKey(
                name: "FK_TakingUserSurveys_Surveys_SurveyId",
                table: "TakingUserSurveys",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakingUserSurveys_Users_UserId",
                table: "TakingUserSurveys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
