namespace Vianditas.Domain.model
{

    public enum EstadoPedido { Pendiente, ListoParaEntrega, Entregado, Cancelado }

    public class Pedido
    {
        private Pedido()
        {
        }

        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Detalles { get; private set; } = null!;
        public EstadoPedido Estado { get; private set; }

        public Pedido(Guid usuarioId, Guid categoriaId, string detalles)
        {
            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Detalles = detalles;
            Estado = EstadoPedido.Pendiente;
        }
    }
}