namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class UsuarioResponseDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NumeroWhatsapp { get; set; } = string.Empty;
    public string WhatsappUserId { get; set; } = string.Empty;
}
