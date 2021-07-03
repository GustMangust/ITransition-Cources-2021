using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CourceProject.Migrations {
  public partial class fanficupdare : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<DateTime>(
          name: "UpdateDate",
          table: "Fanfics",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropColumn(
          name: "UpdateDate",
          table: "Fanfics");
    }
  }
}
