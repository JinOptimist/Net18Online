using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudiosAndLinkToGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudiosId",
                table: "Games",
                type: "int",
                nullable: true);
                                
           

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_StudiosId",
                table: "Games",
                column: "StudiosId");
                    
                      

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Studios_StudiosId",
                table: "Games",
                column: "StudiosId",
                principalTable: "Studios",
                principalColumn: "Id");
                        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Studios_StudiosId",
                table: "Games");
                      

            
            migrationBuilder.DropTable(
                name: "Studios");


            migrationBuilder.DropIndex(
                name: "IX_Games_StudiosId",
                table: "Games");


            migrationBuilder.DropColumn(
                name: "StudiosId",
                table: "Games");

        }
    }
}
