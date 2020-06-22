using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCrawler.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartingPrice = table.Column<string>(maxLength: 10, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Rooms = table.Column<string>(maxLength: 10, nullable: false),
                    Area = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");
        }
    }
}
