namespace Vianditas.Application.Pagos.DTOs;

public class CrearPagoDTO
{
    public Guid PedidoId { get; set; }
    public decimal Monto { get; set; }
}
