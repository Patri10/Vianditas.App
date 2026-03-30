using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vianditas.API.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioWhatsappIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Contrasena",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "NumeroWhatsapp",
                table: "Usuarios",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhatsappUserId",
                table: "Usuarios",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NumeroWhatsapp",
                table: "Usuarios",
                column: "NumeroWhatsapp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_WhatsappUserId",
                table: "Usuarios",
                column: "WhatsappUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_NumeroWhatsapp",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_WhatsappUserId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NumeroWhatsapp",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "WhatsappUserId",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Contrasena",
                table: "Usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);
        }
    }
}
