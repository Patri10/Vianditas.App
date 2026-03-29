namespace Vianditas.Application.Pedidos.Services.DTOs;

public class CrearPedidoCommandDTO
{
    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public List<DetallePedidoCommandDTO> Detalles { get; set; } = new();
    public string? Notas { get; set; }
}

public class DetallePedidoCommandDTO
{
    public Guid MenuId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
