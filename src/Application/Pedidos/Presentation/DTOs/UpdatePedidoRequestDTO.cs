using Vianditas.Domain.model;

namespace Vianditas.Application.Pedidos.Presentation.DTOs;

public class UpdatePedidoRequestDTO
{
    public EstadoPedido? Estado { get; set; }
    public List<DetallePedidoRequestDTO>? Detalles { get; set; }
    public decimal? Total { get; set; }
}