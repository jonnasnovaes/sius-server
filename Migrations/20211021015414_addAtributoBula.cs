using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class addAtributoBula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bula",
                table: "EstoqueVacina",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bula",
                table: "EstoqueVacina");
        }
    }
}
