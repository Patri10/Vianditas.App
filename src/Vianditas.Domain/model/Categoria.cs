namespace Vianditas.Domain.model
{
    public class Categoria
    {
        private Categoria()
        {
        }

        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Categoria(string nombre, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Nombre = nombre;
        }
    }
}