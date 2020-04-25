using Microsoft.EntityFrameworkCore.Migrations;

namespace vkr.Migrations
{
    public partial class ChangePracticalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Practical",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Practical",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Practical",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Practical");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Practical");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Practical");
        }
    }
}
