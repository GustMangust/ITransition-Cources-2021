using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations
{
    public partial class chapterchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalUrl",
                table: "Chapters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalUrl",
                table: "Chapters");
        }
    }
}
