using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class PagoEndpoints
{
    public static void MapPagoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pagos").WithTags("Pagos");

        // GET /api/pagos/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var pago = await db.Pagos.FindAsync(id);
            if (pago is null) return Results.NotFound(new { mensaje = "Pago no encontrado." });

            return Results.Ok(new PagoDto(
                pago.Id, pago.PedidoId, pago.MercadoPagoId,
                pago.LinkdePago, pago.Estado, pago.FechaCreacion));
        })
        .WithName("GetPagoById")
        .WithSummary("Obtiene un pago por ID");

        // GET /api/pagos/pedido/{pedidoId} — Buscar pago por pedido
        group.MapGet("/pedido/{pedidoId:guid}", async (Guid pedidoId, ViandistasDbContext db) =>
        {
            var pago = await db.Pagos.FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
            if (pago is null) return Results.NotFound(new { mensaje = "No se encontró un pago para este pedido." });

            return Results.Ok(new PagoDto(
                pago.Id, pago.PedidoId, pago.MercadoPagoId,
                pago.LinkdePago, pago.Estado, pago.FechaCreacion));
        })
        .WithName("GetPagoByPedido")
        .WithSummary("Obtiene el pago asociado a un pedido")
        .WithDescription("El bot consulta este endpoint para obtener el link de pago y enviárselo al cliente por WhatsApp.");

        // POST /api/pagos — Crear pago para un pedido
        group.MapPost("/", async (CrearPagoDto dto, ViandistasDbContext db) =>
        {
            var pedidoExiste = await db.Pedidos.AnyAsync(p => p.Id == dto.PedidoId);
            if (!pedidoExiste) return Results.BadRequest(new { mensaje = "El pedido no existe." });

            var yaExiste = await db.Pagos.AnyAsync(p => p.PedidoId == dto.PedidoId);
            if (yaExiste) return Results.Conflict(new { mensaje = "Ya existe un pago para este pedido." });

            var pago = new Pago(dto.PedidoId, dto.MercadoPagoId, dto.LinkdePago);
            db.Pagos.Add(pago);
            await db.SaveChangesAsync();

            return Results.Created($"/api/pagos/{pago.Id}", new PagoDto(
                pago.Id, pago.PedidoId, pago.MercadoPagoId,
                pago.LinkdePago, pago.Estado, pago.FechaCreacion));
        })
        .WithName("CrearPago")
        .WithSummary("Crea un pago asociado a un pedido")
        .WithDescription("Crea el registro de pago. El link de MercadoPago puede generarse luego y actualizarse.");

        // PATCH /api/pagos/{id} — Actualizar estado/link del pago (webhook de MercadoPago)
        group.MapPatch("/{id:guid}", async (Guid id, ActualizarPagoDto dto, ViandistasDbContext db) =>
        {
            var pago = await db.Pagos.FindAsync(id);
            if (pago is null) return Results.NotFound(new { mensaje = "Pago no encontrado." });

            if (dto.Estado is not null)
                pago.ActualizarEstado(dto.Estado);

            if (dto.LinkdePago is not null || dto.MercadoPagoId is not null)
                pago.ActualizarLinkPago(dto.LinkdePago, dto.MercadoPagoId);

            await db.SaveChangesAsync();

            return Results.Ok(new { mensaje = "Pago actualizado correctamente." });
        })
        .WithName("ActualizarPago")
        .WithSummary("Actualiza estado y/o link de pago")
        .WithDescription("Se usa para actualizar el estado cuando MercadoPago confirma el pago, o para agregar el link de pago generado.");
    }
}
