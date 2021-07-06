using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations {
  public partial class Chapter_Changed : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<string>(
          name: "ImageUrl",
          table: "Chapters",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropColumn(
          name: "ImageUrl",
          table: "Chapters");
    }
  }
}
