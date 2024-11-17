using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class LoadUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoadUserDataId",
                table: "Metrics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadUserDataId",
                table: "LoadVolumeTestingMetrics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoadUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Coins = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_LoadUserDataId",
                table: "Metrics",
                column: "LoadUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadVolumeTestingMetrics_LoadUserDataId",
                table: "LoadVolumeTestingMetrics",
                column: "LoadUserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadVolumeTestingMetrics_LoadUsers_LoadUserDataId",
                table: "LoadVolumeTestingMetrics",
                column: "LoadUserDataId",
                principalTable: "LoadUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_LoadUsers_LoadUserDataId",
                table: "Metrics",
                column: "LoadUserDataId",
                principalTable: "LoadUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadVolumeTestingMetrics_LoadUsers_LoadUserDataId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_LoadUsers_LoadUserDataId",
                table: "Metrics");

            migrationBuilder.DropTable(
                name: "LoadUsers");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_LoadUserDataId",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_LoadVolumeTestingMetrics_LoadUserDataId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropColumn(
                name: "LoadUserDataId",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "LoadUserDataId",
                table: "LoadVolumeTestingMetrics");
        }
    }
}
