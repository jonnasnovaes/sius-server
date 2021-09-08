using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sius_server.Migrations
{
    public partial class vacina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fabricante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFabricacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacina", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacina");
        }
    }
}
