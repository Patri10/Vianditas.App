namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class UsuarioResponseDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}
