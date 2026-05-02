using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class ComercioEndpoints
{
    public static void MapComercioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/comercios").WithTags("Comercios");

        // GET /api/comercios
        group.MapGet("/", async (ViandistasDbContext db) =>
        {
            var comercios = await db.Comercios
                .Where(c => c.Activo)
                .Select(c => new ComercioDto(c.Id, c.Nombre, c.Direccion, c.Telefono, c.Activo))
                .ToListAsync();
            return Results.Ok(comercios);
        })
        .WithName("GetComercios")
        .WithSummary("Obtiene todos los comercios activos");

        // GET /api/comercios/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var comercio = await db.Comercios.FindAsync(id);
            if (comercio is null) return Results.NotFound(new { mensaje = "Comercio no encontrado." });

            return Results.Ok(new ComercioDto(
                comercio.Id, comercio.Nombre, comercio.Direccion, comercio.Telefono, comercio.Activo));
        })
        .WithName("GetComercioById")
        .WithSummary("Obtiene un comercio por ID");

        // POST /api/comercios
        group.MapPost("/", async (CrearComercioDto dto, ViandistasDbContext db) =>
        {
            var comercio = new Comercio(dto.Nombre, dto.Direccion, dto.Telefono);
            db.Comercios.Add(comercio);
            await db.SaveChangesAsync();

            return Results.Created($"/api/comercios/{comercio.Id}",
                new ComercioDto(comercio.Id, comercio.Nombre, comercio.Direccion, comercio.Telefono, comercio.Activo));
        })
        .WithName("CrearComercio")
        .WithSummary("Crea un nuevo comercio");
    }
}
