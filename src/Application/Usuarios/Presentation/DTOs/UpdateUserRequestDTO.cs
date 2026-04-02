namespace Vianditas.Application.Usuarios.Presentation.DTOs;

public class UpdateUserRequestDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string NumeroWhatsapp { get; set; } = null!;
public string WhatsappUserId { get; set; } = null!;

}