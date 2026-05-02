namespace Vianditas.Domain.DTOs;

public record PedidoDto(
    Guid Id,
    Guid UsuarioId,
    string Estado,
    string Detalles,
    List<DetallePedidoDto> Items
);

public record DetallePedidoDto(
    Guid Id,
    Guid MenuId,
    string MenuNombre,
    int Cantidad,
    double PrecioUnitario,
    double Subtotal
);

public record CrearPedidoDto(
    Guid UsuarioId,
    Guid CategoriaId,
    string Detalles,
    List<CrearDetallePedidoDto> Items
);

public record CrearDetallePedidoDto(
    Guid MenuId,
    int Cantidad,
    double PrecioUnitario
);

public record ActualizarEstadoPedidoDto(
    string Estado
);
