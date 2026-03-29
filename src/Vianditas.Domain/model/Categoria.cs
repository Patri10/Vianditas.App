namespace Vianditas.Domain.model
{
    public class Categoria
    {
        private Categoria()
        {
        }

        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Categoria(string nombre)
        {
            Nombre = nombre;
        }
    }
}