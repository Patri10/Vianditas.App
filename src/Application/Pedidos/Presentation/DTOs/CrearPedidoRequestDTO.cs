namespace Vianditas.Application.Pedidos.Presentation.DTOs;

public class CrearPedidoRequestDTO
{
    public Guid UsuarioId { get; set; }
    public Guid CategoriaId { get; set; }
    public List<DetallePedidoRequestDTO> Detalles { get; set; } = new();
    public string? Notas { get; set; }
}

public class DetallePedidoRequestDTO
{
    public Guid MenuId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
