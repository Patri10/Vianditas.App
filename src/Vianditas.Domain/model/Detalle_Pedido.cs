namespace Vianditas.Domain.model
{

    public class Detalle_Pedido
    {
        private Detalle_Pedido()
        {
        }

        public Guid Id { get; private set; }
        public int Cantidad { get; private set; }

        public double PrecioUnitario { get; private set; }

        public Guid MenuId { get; private set; }
        public Menu Menu { get; private set; } = null!;

        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; } = null!;

        public Detalle_Pedido(int cantidad, double precioUnitario, Guid menuId, Guid? pedidoId = null)
        {
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            MenuId = menuId;
            if (pedidoId.HasValue)
                PedidoId = pedidoId.Value;
        }

    }

}