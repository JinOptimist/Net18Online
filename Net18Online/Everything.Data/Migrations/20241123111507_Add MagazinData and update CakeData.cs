using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMagazinDataandupdateCakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Cakes");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Cakes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cakes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Magazines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magazines_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CakeDataMagazinData",
                columns: table => new
                {
                    CakesId = table.Column<int>(type: "int", nullable: false),
                    MagazinsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CakeDataMagazinData", x => new { x.CakesId, x.MagazinsId });
                    table.ForeignKey(
                        name: "FK_CakeDataMagazinData_Cakes_CakesId",
                        column: x => x.CakesId,
                        principalTable: "Cakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CakeDataMagazinData_Magazines_MagazinsId",
                        column: x => x.MagazinsId,
                        principalTable: "Magazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_CreatorId",
                table: "Cakes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CakeDataMagazinData_MagazinsId",
                table: "CakeDataMagazinData",
                column: "MagazinsId");

            migrationBuilder.CreateIndex(
                name: "IX_Magazines_CreatorId",
                table: "Magazines",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cakes_Users_CreatorId",
                table: "Cakes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cakes_Users_CreatorId",
                table: "Cakes");

            migrationBuilder.DropTable(
                name: "CakeDataMagazinData");

            migrationBuilder.DropTable(
                name: "Magazines");

            migrationBuilder.DropIndex(
                name: "IX_Cakes_CreatorId",
                table: "Cakes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Cakes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cakes");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Cakes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
