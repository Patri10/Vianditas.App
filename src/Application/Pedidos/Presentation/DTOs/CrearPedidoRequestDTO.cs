using Vianditas.Domain.model;

namespace Vianditas.Application.Pedidos.Presentation.DTOs;

public class CrearPedidoRequestDTO
{
    public EstadoPedido Estado { get; set; }

    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public List<DetallePedidoRequestDTO> Detalles { get; set; } = new();
    
    public decimal Total { get; set; }  

    public DateTime HoraCreacion { get; set; } = DateTime.UtcNow;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
}

public class DetallePedidoRequestDTO
{
    public Guid MenuId { get; set; }
    public int Cantidad { get; set; }        
    public decimal PrecioUnitario { get; set; }
}
