using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class AtualizacaoEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fabricante",
                table: "EstoqueVacina",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "EstoqueVacina",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fabricante",
                table: "EstoqueVacina");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "EstoqueVacina");
        }
    }
}
