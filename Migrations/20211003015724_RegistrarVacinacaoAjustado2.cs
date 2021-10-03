using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class RegistrarVacinacaoAjustado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataVacinacao",
                table: "RegistrarVacinacao",
                newName: "DataVacinacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataVacinacao",
                table: "RegistrarVacinacao",
                newName: "dataVacinacao");
        }
    }
}
