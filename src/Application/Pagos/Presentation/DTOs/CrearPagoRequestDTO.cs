namespace Vianditas.Application.Pagos.Presentation.DTOs;

public class CrearPagoRequestDTO
{
    public Guid PedidoId { get; set; }
    public decimal Monto { get; set; }
}
