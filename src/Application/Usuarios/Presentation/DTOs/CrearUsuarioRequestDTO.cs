namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class CrearUsuarioRequestDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string NumeroWhatsapp { get; set; } = string.Empty;
    public string WhatsappUserId { get; set; } = string.Empty;
}
