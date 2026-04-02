namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class UpdateUserCommandDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string NumeroWhatsapp { get; set; } = null!;
    public string WhatsappUserId { get; set; } = null!;
}