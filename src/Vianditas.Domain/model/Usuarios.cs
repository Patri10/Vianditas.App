namespace Vianditas.Domain.model;

public class Usuarios
{
    private Usuarios()
    {
    }

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string NumeroWhatsapp { get; private set; } = null!;
    public string WhatsappUserId { get; private set; } = null!;

    public Usuarios(string nombre, string numeroWhatsapp, string whatsappUserId)
    {
        Nombre = nombre;
        NumeroWhatsapp = numeroWhatsapp;
        WhatsappUserId = whatsappUserId;
    }

    public void Update(string nombre, string numeroWhatsapp, string whatsappUserId)
    {
        Nombre = nombre;
        NumeroWhatsapp = numeroWhatsapp;
        WhatsappUserId = whatsappUserId;
    }
}
