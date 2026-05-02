using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pedidos").WithTags("Pedidos");

        // GET /api/pedidos — Lista todos los pedidos
        group.MapGet("/", async (ViandistasDbContext db) =>
        {
            var pedidos = await db.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Menu)
                .Select(p => new PedidoDto(
                    p.Id,
                    p.UsuarioId,
                    p.Estado.ToString(),
                    p.Detalles,
                    p.DetallePedidos.Select(d => new DetallePedidoDto(
                        d.Id,
                        d.MenuId,
                        d.Menu.Nombre,
                        d.Cantidad,
                        d.PrecioUnitario,
                        d.Cantidad * d.PrecioUnitario
                    )).ToList()
                ))
                .ToListAsync();

            return Results.Ok(pedidos);
        })
        .WithName("GetPedidos")
        .WithSummary("Obtiene todos los pedidos");

        // GET /api/pedidos/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var pedido = await db.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Menu)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido is null) return Results.NotFound(new { mensaje = "Pedido no encontrado." });

            return Results.Ok(new PedidoDto(
                pedido.Id,
                pedido.UsuarioId,
                pedido.Estado.ToString(),
                pedido.Detalles,
                pedido.DetallePedidos.Select(d => new DetallePedidoDto(
                    d.Id,
                    d.MenuId,
                    d.Menu.Nombre,
                    d.Cantidad,
                    d.PrecioUnitario,
                    d.Cantidad * d.PrecioUnitario
                )).ToList()
            ));
        })
        .WithName("GetPedidoById")
        .WithSummary("Obtiene un pedido por ID");

        // GET /api/pedidos/usuario/{usuarioId} — Pedidos de un usuario (para el bot)
        group.MapGet("/usuario/{usuarioId:guid}", async (Guid usuarioId, ViandistasDbContext db) =>
        {
            var pedidos = await db.Pedidos
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.Menu)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Id)
                .Select(p => new PedidoDto(
                    p.Id,
                    p.UsuarioId,
                    p.Estado.ToString(),
                    p.Detalles,
                    p.DetallePedidos.Select(d => new DetallePedidoDto(
                        d.Id,
                        d.MenuId,
                        d.Menu.Nombre,
                        d.Cantidad,
                        d.PrecioUnitario,
                        d.Cantidad * d.PrecioUnitario
                    )).ToList()
                ))
                .ToListAsync();

            return Results.Ok(pedidos);
        })
        .WithName("GetPedidosByUsuario")
        .WithSummary("Obtiene todos los pedidos de un usuario")
        .WithDescription("Endpoint clave para el bot: permite consultar el historial de pedidos de un cliente.");

        // POST /api/pedidos — Crear pedido desde el bot
        group.MapPost("/", async (CrearPedidoDto dto, ViandistasDbContext db) =>
        {
            var usuarioExiste = await db.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId);
            if (!usuarioExiste) return Results.BadRequest(new { mensaje = "El usuario no existe." });

            var categoriaExiste = await db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
            if (!categoriaExiste) return Results.BadRequest(new { mensaje = "La categoría no existe." });

            var pedido = new Pedido(dto.UsuarioId, dto.CategoriaId, dto.Detalles);

            // Agregar detalles al objeto en memoria antes de guardar
            foreach (var item in dto.Items)
            {
                var menuExiste = await db.Menus.AnyAsync(m => m.Id == item.MenuId);
                if (!menuExiste) return Results.BadRequest(new { mensaje = $"El menú {item.MenuId} no existe." });

                var detalle = new Detalle_Pedido(item.Cantidad, item.PrecioUnitario, item.MenuId);
                pedido.AgregarDetalle(detalle);
            }

            db.Pedidos.Add(pedido);
            await db.SaveChangesAsync();

            return Results.Created($"/api/pedidos/{pedido.Id}", new { pedido.Id, mensaje = "Pedido creado correctamente." });
        })
        .WithName("CrearPedido")
        .WithSummary("Crea un nuevo pedido")
        .WithDescription("El bot de WhatsApp llama a este endpoint cuando el cliente confirma su pedido.");

        // PATCH /api/pedidos/{id}/estado — Actualizar estado del pedido
        group.MapPatch("/{id:guid}/estado", async (Guid id, ActualizarEstadoPedidoDto dto, ViandistasDbContext db) =>
        {
            var pedido = await db.Pedidos.FindAsync(id);
            if (pedido is null) return Results.NotFound(new { mensaje = "Pedido no encontrado." });

            if (!Enum.TryParse<EstadoPedido>(dto.Estado, ignoreCase: true, out var nuevoEstado))
                return Results.BadRequest(new { mensaje = $"Estado inválido. Valores válidos: {string.Join(", ", Enum.GetNames<EstadoPedido>())}" });

            pedido.ActualizarEstado(nuevoEstado);

            await db.SaveChangesAsync();

            return Results.Ok(new { mensaje = $"Estado actualizado a '{nuevoEstado}'." });
        })
        .WithName("ActualizarEstadoPedido")
        .WithSummary("Actualiza el estado de un pedido")
        .WithDescription("Permite cambiar el estado: Pendiente, ListoParaEntrega, Entregado, Cancelado. El bot notifica al cliente cuando cambia.");
    }
}
