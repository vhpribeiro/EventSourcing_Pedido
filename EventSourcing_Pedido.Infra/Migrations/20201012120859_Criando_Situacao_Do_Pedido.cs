using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventSourcing_Pedido.Infra.Migrations
{
    public partial class Criando_Situacao_Do_Pedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Pedidos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Pedidos");
        }
    }
}
