using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateCoffeCompanyAndActivityAndCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Coffe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Coffe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoffeCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeCompanies_Activities_TypeOfActivityId",
                        column: x => x.TypeOfActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coffe_CompanyId",
                table: "Coffe",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Coffe_CreatorId",
                table: "Coffe",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeCompanies_TypeOfActivityId",
                table: "CoffeCompanies",
                column: "TypeOfActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffe_CoffeCompanies_CompanyId",
                table: "Coffe",
                column: "CompanyId",
                principalTable: "CoffeCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffe_Users_CreatorId",
                table: "Coffe",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffe_CoffeCompanies_CompanyId",
                table: "Coffe");

            migrationBuilder.DropForeignKey(
                name: "FK_Coffe_Users_CreatorId",
                table: "Coffe");

            migrationBuilder.DropTable(
                name: "CoffeCompanies");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Coffe_CompanyId",
                table: "Coffe");

            migrationBuilder.DropIndex(
                name: "IX_Coffe_CreatorId",
                table: "Coffe");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Coffe");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Coffe");
        }
    }
}
