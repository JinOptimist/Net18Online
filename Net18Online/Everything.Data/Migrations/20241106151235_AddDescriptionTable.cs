using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_DescriptionId",
                table: "Animes",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Description_DescriptionId",
                table: "Animes",
                column: "DescriptionId",
                principalTable: "Description",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Description_DescriptionId",
                table: "Animes");

            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropIndex(
                name: "IX_Animes_DescriptionId",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Animes");
        }
    }
}
