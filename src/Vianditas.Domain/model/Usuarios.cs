namespace Vianditas.Domain.model;

public class Usuarios
{
    public Guid Id { get; private set; }
    public string? Nombre { get; private set; }
    public string? Correo { get; private set; }
    public string? Contraseña { get; private set; }
}

