using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLikesMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoadUserDataMetricData",
                columns: table => new
                {
                    MetricsWhichUsersLikeId = table.Column<int>(type: "int", nullable: false),
                    UserWhoLikeItId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadUserDataMetricData", x => new { x.MetricsWhichUsersLikeId, x.UserWhoLikeItId });
                    table.ForeignKey(
                        name: "FK_LoadUserDataMetricData_LoadUsers_UserWhoLikeItId",
                        column: x => x.UserWhoLikeItId,
                        principalTable: "LoadUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoadUserDataMetricData_Metrics_MetricsWhichUsersLikeId",
                        column: x => x.MetricsWhichUsersLikeId,
                        principalTable: "Metrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadUserDataMetricData_UserWhoLikeItId",
                table: "LoadUserDataMetricData",
                column: "UserWhoLikeItId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadUserDataMetricData");
        }
    }
}
