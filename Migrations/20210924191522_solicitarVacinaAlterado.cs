using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class solicitarVacinaAlterado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "recebido",
                table: "SolicitarVacina",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recebido",
                table: "SolicitarVacina");
        }
    }
}
