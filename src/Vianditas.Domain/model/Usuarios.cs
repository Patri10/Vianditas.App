namespace Vianditas.Domain.model;



public class Usuarios
{
    private Usuarios()
    {
    }

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Correo { get; private set; } = null!;
    public string Contrasena { get; private set; } = null!;

    public Usuarios(string nombre, string correo, string contrasena, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Nombre = nombre;
        Correo = correo;
        Contrasena = contrasena;
    }
}
