using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerToQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakingUserSurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerToQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnswerToQuestions_TakingUserSurveys_TakingUserSurveyId",
                        column: x => x.TakingUserSurveyId,
                        principalTable: "TakingUserSurveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestions_QuestionId",
                table: "AnswerToQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestions_TakingUserSurveyId",
                table: "AnswerToQuestions",
                column: "TakingUserSurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerToQuestions");
        }
    }
}
