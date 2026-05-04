namespace Vianditas.Domain.model;

public class Cliente
{
    private Cliente() { }

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Apellido { get; private set; } = null!;
    public string Telefono { get; private set; } = null!;
    public string? Direccion { get; private set; }

    public Cliente(string nombre, string apellido, string telefono, string? direccion = null)
    {
        Nombre = nombre;
        Apellido = apellido;
        Telefono = telefono;
        Direccion = direccion;
    }

    public void ActualizarDatos(string nombre, string apellido, string? direccion)
    {
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
    }
}
