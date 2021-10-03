using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class RegistrarVacinacaoAjustado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVacina",
                table: "RegistrarVacinacao");

            migrationBuilder.AddColumn<string>(
                name: "Vacina",
                table: "RegistrarVacinacao",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vacina",
                table: "RegistrarVacinacao");

            migrationBuilder.AddColumn<int>(
                name: "IdVacina",
                table: "RegistrarVacinacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
