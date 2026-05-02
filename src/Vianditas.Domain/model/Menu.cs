namespace Vianditas.Domain.model
{
    public class Menu
    {
        private Menu()
        {
        }

        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public decimal Precio { get; private set; }
        public string Descripcion { get; private set; } = null!;
        public bool Activo { get; private set; } = true;

        public Guid ComercioId { get; private set; }
        public Comercio Comercio { get; private set; } = null!;

        public Guid CategoriaId { get; private set; }

        public Categoria Categoria { get; private set; } = null!;

        public Menu(string nombre, decimal precio, string descripcion, Guid comercioId, Guid categoriaId)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            ComercioId = comercioId;
            CategoriaId = categoriaId;
            Activo = true;
        }

        // Métodos de dominio
        public void Desactivar() => Activo = false;
        public void Activar() => Activo = true;
    }
}