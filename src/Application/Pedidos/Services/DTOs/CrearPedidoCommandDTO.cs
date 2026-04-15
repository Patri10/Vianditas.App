using Vianditas.Domain.model;

namespace Vianditas.Application.Pedidos.Services.DTOs;

public class CrearPedidoCommandDTO
{
    public EstadoPedido Estado { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public List<DetallePedidoCommandDTO> Detalles { get; set; } = new();
    public decimal Total { get; set; }

    public DateTime HoraCreacion { get; set; } = DateTime.UtcNow;
}

public class DetallePedidoCommandDTO
{

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public Guid MenuId { get; set; }

    public Guid PedidoId { get; set; }
}
