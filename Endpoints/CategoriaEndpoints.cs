using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class CategoriaEndpoints
{
    public static void MapCategoriaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categorias").WithTags("Categorias");

        // GET /api/categorias
        group.MapGet("/", async (ViandistasDbContext db) =>
        {
            var categorias = await db.Categorias
                .Select(c => new CategoriaDto(c.Id, c.Nombre))
                .ToListAsync();
            return Results.Ok(categorias);
        })
        .WithName("GetCategorias")
        .WithSummary("Obtiene todas las categorías")
        .WithDescription("El bot lista las categorías disponibles para que el cliente elija antes de ver el menú.");

        // GET /api/categorias/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var cat = await db.Categorias.FindAsync(id);
            if (cat is null) return Results.NotFound(new { mensaje = "Categoría no encontrada." });
            return Results.Ok(new CategoriaDto(cat.Id, cat.Nombre));
        })
        .WithName("GetCategoriaById")
        .WithSummary("Obtiene una categoría por ID");

        // POST /api/categorias
        group.MapPost("/", async (CrearCategoriaDto dto, ViandistasDbContext db) =>
        {
            var existe = await db.Categorias.AnyAsync(c => c.Nombre == dto.Nombre);
            if (existe) return Results.Conflict(new { mensaje = "Ya existe una categoría con ese nombre." });

            var categoria = new Categoria(dto.Nombre);
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            return Results.Created($"/api/categorias/{categoria.Id}",
                new CategoriaDto(categoria.Id, categoria.Nombre));
        })
        .WithName("CrearCategoria")
        .WithSummary("Crea una nueva categoría");
    }
}
