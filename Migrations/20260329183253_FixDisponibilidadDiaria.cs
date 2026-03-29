using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vianditas.API.Migrations
{
    /// <inheritdoc />
    public partial class FixDisponibilidadDiaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisponibilidadesDiarias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false),
                    MenuId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisponibilidadesDiarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisponibilidadesDiarias_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisponibilidadesDiarias_MenuId_Fecha",
                table: "DisponibilidadesDiarias",
                columns: new[] { "MenuId", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisponibilidadesDiarias");
        }
    }
}
