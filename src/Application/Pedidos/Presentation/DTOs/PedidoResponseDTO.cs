using Vianditas.Domain.model;

namespace Vianditas.Application.Pedidos.Presentation.DTOs;

public class PedidoResponseDTO
{
    public Guid Id { get; set; }
    public EstadoPedido Estado { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public decimal Total { get; set; }
    public DateTime HoraCreacion { get; set; }

    public List<DetallePedidoResponseDTO> Detalles { get; set; } = new();
}

public class DetallePedidoResponseDTO
{
    public Guid MenuId { get; set; }
    public string PlatoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
