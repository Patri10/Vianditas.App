namespace Vianditas.Application.Pedidos.DTOs;

public class CrearPedidoDTO
{
    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public List<DetallePedidoDTO> Detalles { get; set; } = new();
    public string? Notas { get; set; }
}

public class DetallePedidoDTO
{
    public Guid MenuId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
