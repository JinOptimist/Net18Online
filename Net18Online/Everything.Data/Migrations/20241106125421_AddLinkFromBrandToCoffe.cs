using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkFromBrandToCoffe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Coffe");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Coffe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coffe_BrandId",
                table: "Coffe",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffe_Brands_BrandId",
                table: "Coffe",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffe_Brands_BrandId",
                table: "Coffe");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Coffe_BrandId",
                table: "Coffe");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Coffe");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Coffe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
