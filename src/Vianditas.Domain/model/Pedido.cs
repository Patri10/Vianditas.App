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

        // Navegación
        public ICollection<Detalle_Pedido> DetallePedidos { get; private set; } = new List<Detalle_Pedido>();
        public Pago? Pago { get; private set; }

        public Pedido(Guid usuarioId, Guid categoriaId, string detalles)
        {
            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Detalles = detalles;
            Estado = EstadoPedido.Pendiente;
        }

        public void AgregarDetalle(Detalle_Pedido detalle)
        {
            DetallePedidos.Add(detalle);
        }

        // Métodos de dominio
        public void ActualizarEstado(EstadoPedido nuevoEstado)
        {
            Estado = nuevoEstado;
        }
    }
}