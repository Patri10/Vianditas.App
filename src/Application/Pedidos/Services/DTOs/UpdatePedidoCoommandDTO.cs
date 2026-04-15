namespace Vianditas.Application.Pedidos.Services.DTOs;

using Vianditas.Domain.model;
public class UpdatePedidoCommandDTO
{
    public EstadoPedido? Estado { get; set; }
    public List<DetallePedidoCommandDTO>? Detalles { get; set; }
    public decimal? Total { get; set; }
}