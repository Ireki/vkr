using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vkr.Migrations
{
    public partial class AddTableOtherDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtherDocumentation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDocumentation = table.Column<string>(nullable: true),
                    DateDeposit = table.Column<DateTime>(nullable: false),
                    ShelfLife = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherDocumentation");
        }
    }
}
