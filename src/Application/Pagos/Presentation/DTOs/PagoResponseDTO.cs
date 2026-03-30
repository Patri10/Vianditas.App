namespace Vianditas.Application.Pagos.Presentation.DTOs;

public class PagoResponseDTO
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public decimal Monto { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaCreacion { get; set; }
    public string? LinkDePago { get; set; }
    public string? MercadoPagoId { get; set; }
}
