using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class VolumeTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoadVolumeTestingId",
                table: "Metrics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoadVolumeTestingMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadVolumeTestingMetrics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_LoadVolumeTestingId",
                table: "Metrics",
                column: "LoadVolumeTestingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_LoadVolumeTestingMetrics_LoadVolumeTestingId",
                table: "Metrics",
                column: "LoadVolumeTestingId",
                principalTable: "LoadVolumeTestingMetrics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_LoadVolumeTestingMetrics_LoadVolumeTestingId",
                table: "Metrics");

            migrationBuilder.DropTable(
                name: "LoadVolumeTestingMetrics");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_LoadVolumeTestingId",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "LoadVolumeTestingId",
                table: "Metrics");
        }
    }
}
