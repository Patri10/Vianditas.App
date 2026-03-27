namespace Vianditas.Domain.model
{

    public enum EstadoPedido { Pendiente, ListoParaEntrega, Entregado, Cancelado }

    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public decimal Total { get; private set; }
        public EstadoPedido Estado { get; private set; } = EstadoPedido.Pendiente;

        public Guid UsuarioId { get; private set; }
        public Usuarios Usuario { get; private set; }

        public ICollection<DetallePedido> DetallePedido { get; private set; } = new List<DetallePedido>();

        public Pago? Pago { get; private set; }
    }

    public class DetallePedido
    {
        public Guid Id { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        public Guid MenuId { get; private set; }
        public Menu Menu { get; private set; }

        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
    }
}