namespace Vianditas.Domain.model
{
    public class Pago
    {
        private Pago()
        {
        }

        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; } = null!;

        public string? MercadoPagoId { get; private set; }
        public string? LinkdePago { get; private set; }

        public string? Estado { get; private set; } = "Pendiente";

        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

        public Pago(Guid pedidoId, string? mercadoPagoId = null, string? linkdePago = null)
        {
            PedidoId = pedidoId;
            MercadoPagoId = mercadoPagoId;
            LinkdePago = linkdePago;
            Estado = "Pendiente";
            FechaCreacion = DateTime.UtcNow;
        }

        // Métodos de dominio
        public void ActualizarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public void ActualizarLinkPago(string? linkdePago, string? mercadoPagoId = null)
        {
            LinkdePago = linkdePago;
            if (mercadoPagoId is not null)
                MercadoPagoId = mercadoPagoId;
        }
    }
}