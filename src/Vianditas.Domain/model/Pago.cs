namespace Vianditas.Domain.model
{
    public class Pago
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }

        public string? MercadoPagoId { get; private set; }
        public string? LinkdePago { get; private set; }

        public string? Estado { get; private set; } = "Pendiente";

        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;
    }
}