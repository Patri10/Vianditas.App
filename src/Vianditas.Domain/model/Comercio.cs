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
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del comercio no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La dirección del comercio no puede estar vacía.", nameof(direccion));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono del comercio no puede estar vacío.", nameof(telefono));

            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Activo = true;
        }
    }
}