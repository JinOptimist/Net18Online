using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedaNumberOfImagesCreatedcolumntotheUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfImagesCreated",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfImagesCreated",
                table: "Users");
        }
    }
}
