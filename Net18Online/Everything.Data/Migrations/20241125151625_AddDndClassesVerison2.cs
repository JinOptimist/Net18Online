using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDndClassesVerison2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DndClasses",
                table: "DndClasses");

            migrationBuilder.RenameTable(
                name: "DndClasses",
                newName: "DndClassesNewVersion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DndClassesNewVersion",
                table: "DndClassesNewVersion",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DndClassesNewVersion",
                table: "DndClassesNewVersion");

            migrationBuilder.RenameTable(
                name: "DndClassesNewVersion",
                newName: "DndClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DndClasses",
                table: "DndClasses",
                column: "Id");
        }
    }
}
