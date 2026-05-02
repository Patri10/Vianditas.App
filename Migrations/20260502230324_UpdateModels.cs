using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vianditas.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId1",
                table: "Pagos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId1",
                table: "DetallePedidos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_PedidoId1",
                table: "Pagos",
                column: "PedidoId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_PedidoId1",
                table: "DetallePedidos",
                column: "PedidoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId1",
                table: "DetallePedidos",
                column: "PedidoId1",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId1",
                table: "Pagos",
                column: "PedidoId1",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId1",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Pedidos_PedidoId1",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_PedidoId1",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_PedidoId1",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "PedidoId1",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "PedidoId1",
                table: "DetallePedidos");
        }
    }
}
