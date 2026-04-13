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


        public void Update(string nombre, decimal precio, string descripcion, bool activo)
        {
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            Activo = activo;
        }

        public void UpdateName(string nombre)
        {
            Nombre = nombre;
        }

        public void UpdatePrice(decimal precio)
        {
            Precio = precio;
        }

        public void UpdateDescription(string descripcion)
        {
            Descripcion = descripcion;
        }

    }
}