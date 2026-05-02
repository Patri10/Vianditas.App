namespace Vianditas.Domain.DTOs;

public record PagoDto(
    Guid Id,
    Guid PedidoId,
    string? MercadoPagoId,
    string? LinkdePago,
    string? Estado,
    DateTime FechaCreacion
);

public record CrearPagoDto(
    Guid PedidoId,
    string? MercadoPagoId = null,
    string? LinkdePago = null
);

public record ActualizarPagoDto(
    string Estado,
    string? MercadoPagoId = null,
    string? LinkdePago = null
);
