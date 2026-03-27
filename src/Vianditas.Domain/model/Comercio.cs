namespace Vianditas.Domain.model
{
    public class Comercio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }

        public bool Activo { get; private set; } = true;
        public string Telefono { get; private set; }

        public List<Menu> Menus { get; private set; }
    }
}