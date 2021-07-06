using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations {
  public partial class fanfictags : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "FanficTags",
          columns: table => new {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            UserId = table.Column<string>(nullable: true),
            TagId = table.Column<int>(nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_FanficTags", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "FanficTags");
    }
  }
}
