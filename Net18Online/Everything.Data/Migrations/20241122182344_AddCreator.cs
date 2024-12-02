using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadVolumeTestingMetrics_LoadUsers_LoadUserDataId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_LoadUsers_LoadUserDataId",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_LoadVolumeTestingMetrics_LoadUserDataId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropColumn(
                name: "LoadUserDataId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.RenameColumn(
                name: "LoadUserDataId",
                table: "Metrics",
                newName: "LoadUserDataCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_LoadUserDataId",
                table: "Metrics",
                newName: "IX_Metrics_LoadUserDataCreatorId");

            migrationBuilder.AddColumn<int>(
                name: "LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoadVolumeTestingMetrics_LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics",
                column: "LoadUserDataCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadVolumeTestingMetrics_LoadUsers_LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics",
                column: "LoadUserDataCreatorId",
                principalTable: "LoadUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_LoadUsers_LoadUserDataCreatorId",
                table: "Metrics",
                column: "LoadUserDataCreatorId",
                principalTable: "LoadUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadVolumeTestingMetrics_LoadUsers_LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_LoadUsers_LoadUserDataCreatorId",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_LoadVolumeTestingMetrics_LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.DropColumn(
                name: "LoadUserDataCreatorId",
                table: "LoadVolumeTestingMetrics");

            migrationBuilder.RenameColumn(
                name: "LoadUserDataCreatorId",
                table: "Metrics",
                newName: "LoadUserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_LoadUserDataCreatorId",
                table: "Metrics",
                newName: "IX_Metrics_LoadUserDataId");

            migrationBuilder.AddColumn<int>(
                name: "LoadUserDataId",
                table: "LoadVolumeTestingMetrics",
                type: "int",
                nullable: true);

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
    }
}
