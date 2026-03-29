namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class CrearUsuarioRequestDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
