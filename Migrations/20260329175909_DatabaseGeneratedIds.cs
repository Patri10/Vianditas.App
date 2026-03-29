using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vianditas.API.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseGeneratedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "Contrasena");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaId",
                table: "Pedidos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Detalles",
                table: "Pedidos",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CategoriaId",
                table: "Pedidos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Categorias_CategoriaId",
                table: "Pedidos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Categorias_CategoriaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CategoriaId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Detalles",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "Contrasena",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Pedidos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Pedidos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
