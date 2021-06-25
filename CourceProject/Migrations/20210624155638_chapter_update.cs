using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations
{
    public partial class chapter_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Work_Id",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "Fanfic_Id",
                table: "Chapters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fanfic_Id",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "Work_Id",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
