namespace Vianditas.Domain.model
{

    public enum EstadoPedido { Pendiente, ListoParaEntrega, Entregado, Cancelado }

    public class Pedido
    {
        private Pedido()
        {
        }

        public Guid Id { get; private set; }
        public EstadoPedido Estado { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string Detalles { get; private set; } = null!;

        public decimal Total { get; private set; }

        public DateTime HoraCreacion { get; private set; } = DateTime.UtcNow;

        public Pedido(EstadoPedido estado, Guid usuarioId, Guid categoriaId, string detalles, decimal total, DateTime horaCreacion)
        {
            Estado = estado;
            UsuarioId = usuarioId;
            CategoriaId = categoriaId;
            Detalles = detalles;
            Total = total;
            HoraCreacion = horaCreacion;
        }

        public void Update(EstadoPedido? estado, string? detalles, decimal? total)
        {
            if (estado.HasValue)
            {
                Estado = estado.Value;
            }

            if (!string.IsNullOrWhiteSpace(detalles))
            {
                Detalles = detalles;
            }

            if (total.HasValue)
            {
                Total = total.Value;
            }
        }
    }
}