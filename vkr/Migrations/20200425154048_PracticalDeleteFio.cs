using Microsoft.EntityFrameworkCore.Migrations;

namespace vkr.Migrations
{
    public partial class PracticalDeleteFio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fio",
                table: "Practical");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fio",
                table: "Practical",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
