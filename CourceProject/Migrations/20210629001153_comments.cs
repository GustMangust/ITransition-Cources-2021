﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CourceProject.Migrations {
  public partial class comments : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "Comments",
          columns: table => new {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Body = table.Column<string>(nullable: true),
            User_Id = table.Column<string>(nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Comments", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "Comments");
    }
  }
}
