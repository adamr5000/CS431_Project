using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CS431_Project.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "thing",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Npgsql:Serial", true),
                    value = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thing", x => x.id);
                });
            migrationBuilder.CreateTable(
                name: "interest",
                columns: table => new
                {
                    id = table.Column<int>(isNullable: false)
                        .Annotation("Npgsql:Serial", true),
                    age = table.Column<int>(isNullable: true),
                    name = table.Column<string>(isNullable: true),
                    otherthingthingId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.id);
                    table.ForeignKey(
                        name: "FK_Interest_thing_otherthingthingId",
                        column: x => x.otherthingthingId,
                        principalTable: "thing",
                        principalColumn: "id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("interest");
            migrationBuilder.DropTable("thing");
        }
    }
}
