using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations {
  public partial class @new : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropColumn(
          name: "Fandom",
          table: "Fanfics");

      migrationBuilder.AddColumn<string>(
          name: "Fandom_Id",
          table: "Fanfics",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "Fandoms",
          columns: table => new {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Fandoms", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Tags",
          columns: table => new {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Tags", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "Fandoms");

      migrationBuilder.DropTable(
          name: "Tags");

      migrationBuilder.DropColumn(
          name: "Fandom_Id",
          table: "Fanfics");

      migrationBuilder.AddColumn<string>(
          name: "Fandom",
          table: "Fanfics",
          type: "nvarchar(max)",
          nullable: true);
    }
  }
}
