namespace Vianditas.Application.Usuarios.Services.DTOs;

public class CrearUsuarioCommandDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
