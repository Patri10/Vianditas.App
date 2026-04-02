namespace Vianditas.Application.Usuarios.Services.DTOs;

public class CreateUserCommandDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string NumeroWhatsapp { get; set; } = string.Empty;
    public string WhatsappUserId { get; set; } = string.Empty;
}
