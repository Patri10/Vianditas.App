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
        SetNombre(nombre);
        SetNumeroWhatsapp(numeroWhatsapp);
        SetWhatsappUserId(whatsappUserId);
    }

    public void Update(string nombre, string numeroWhatsapp, string whatsappUserId)
    {
        SetNombre(nombre);
        SetNumeroWhatsapp(numeroWhatsapp);
        SetWhatsappUserId(whatsappUserId);
    }

    private void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
        }

        Nombre = nombre;
    }

    private void SetNumeroWhatsapp(string numeroWhatsapp)
    {
        if (string.IsNullOrWhiteSpace(numeroWhatsapp))
        {
            throw new ArgumentException("El número de WhatsApp no puede estar vacío.", nameof(numeroWhatsapp));
        }

        NumeroWhatsapp = numeroWhatsapp;
    }

    private void SetWhatsappUserId(string whatsappUserId)
    {
        if (string.IsNullOrWhiteSpace(whatsappUserId))
        {
            throw new ArgumentException("El ID de usuario de WhatsApp no puede estar vacío.", nameof(whatsappUserId));
        }

        WhatsappUserId = whatsappUserId;
    }
}
