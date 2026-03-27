namespace Vianditas.Domain.model
{
    public class Menu
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        public string Descripcion { get; private set; }
        public bool Activo { get; private set; } = true;

        public Guid ComercioId { get; private set; }
        public Comercio Comercio { get; private set; }

        public Guid CategoriaId { get; private set; }

        public Categoria Categoria { get; private set; }

    }
}