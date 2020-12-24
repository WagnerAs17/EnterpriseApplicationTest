using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volvo.Frota.API.Migrations
{
    public partial class DeleteLogico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Caminhoes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExcluidoEm",
                table: "Caminhoes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Caminhoes");

            migrationBuilder.DropColumn(
                name: "ExcluidoEm",
                table: "Caminhoes");
        }
    }
}
