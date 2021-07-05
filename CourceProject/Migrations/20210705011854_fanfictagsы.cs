using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations
{
    public partial class fanfictagsы : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FanficTags");

            migrationBuilder.AddColumn<int>(
                name: "FanficId",
                table: "FanficTags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FanficId",
                table: "FanficTags");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FanficTags",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
