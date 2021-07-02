using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations {
  public partial class bookmarks : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "Bookmarks",
          columns: table => new {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            FanficId = table.Column<int>(nullable: false),
            UserId = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Bookmarks", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "Bookmarks");
    }
  }
}
