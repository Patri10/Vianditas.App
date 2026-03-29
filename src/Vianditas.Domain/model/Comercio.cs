namespace Vianditas.Domain.model
{
    public class Comercio
    {
        private Comercio()
        {
        }

        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public string Direccion { get; private set; } = null!;

        public bool Activo { get; private set; } = true;
        public string Telefono { get; private set; } = null!;

        public List<Menu> Menus { get; private set; } = new();

        public Comercio(string nombre, string direccion, string telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Activo = true;
        }
    }
}