using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class liberarlote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantidade",
                table: "SolicitarVacina");

            migrationBuilder.AddColumn<bool>(
                name: "liberado",
                table: "SolicitarVacina",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "liberado",
                table: "SolicitarVacina");

            migrationBuilder.AddColumn<int>(
                name: "quantidade",
                table: "SolicitarVacina",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
