namespace Vianditas.Application.Pagos.Services.DTOs;

public class CrearPagoCommandDTO
{
    public Guid PedidoId { get; set; }
    public decimal Monto { get; set; }
}
