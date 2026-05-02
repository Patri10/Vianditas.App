using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class MenuEndpoints
{
    public static void MapMenuEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/menus").WithTags("Menus");

        // GET /api/menus — Lista todos los menús activos
        group.MapGet("/", async (ViandistasDbContext db) =>
        {
            var menus = await db.Menus
                .Include(m => m.Categoria)
                .Include(m => m.Comercio)
                .Where(m => m.Activo)
                .Select(m => new MenuDto(
                    m.Id,
                    m.Nombre,
                    m.Precio,
                    m.Descripcion,
                    m.Categoria.Nombre,
                    m.Comercio.Nombre,
                    m.Activo
                ))
                .ToListAsync();

            return Results.Ok(menus);
        })
        .WithName("GetMenus")
        .WithSummary("Obtiene todos los menús activos")
        .WithDescription("Retorna el listado completo de menús activos con su categoría y comercio. Usado por el bot para mostrar opciones al cliente.");

        // GET /api/menus/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var menu = await db.Menus
                .Include(m => m.Categoria)
                .Include(m => m.Comercio)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menu is null) return Results.NotFound(new { mensaje = "Menú no encontrado." });

            return Results.Ok(new MenuDto(
                menu.Id,
                menu.Nombre,
                menu.Precio,
                menu.Descripcion,
                menu.Categoria.Nombre,
                menu.Comercio.Nombre,
                menu.Activo
            ));
        })
        .WithName("GetMenuById")
        .WithSummary("Obtiene un menú por ID");

        // GET /api/menus/categoria/{categoriaId} — Filtrar por categoría (útil para el bot)
        group.MapGet("/categoria/{categoriaId:guid}", async (Guid categoriaId, ViandistasDbContext db) =>
        {
            var menus = await db.Menus
                .Include(m => m.Categoria)
                .Include(m => m.Comercio)
                .Where(m => m.CategoriaId == categoriaId && m.Activo)
                .Select(m => new MenuDto(
                    m.Id,
                    m.Nombre,
                    m.Precio,
                    m.Descripcion,
                    m.Categoria.Nombre,
                    m.Comercio.Nombre,
                    m.Activo
                ))
                .ToListAsync();

            return Results.Ok(menus);
        })
        .WithName("GetMenusByCategoria")
        .WithSummary("Filtra menús por categoría");

        // POST /api/menus — Crear menú
        group.MapPost("/", async (CrearMenuDto dto, ViandistasDbContext db) =>
        {
            var categoriaExiste = await db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
            if (!categoriaExiste) return Results.BadRequest(new { mensaje = "La categoría no existe." });

            var comercioExiste = await db.Comercios.AnyAsync(c => c.Id == dto.ComercioId);
            if (!comercioExiste) return Results.BadRequest(new { mensaje = "El comercio no existe." });

            var menu = new Menu(dto.Nombre, dto.Precio, dto.Descripcion, dto.ComercioId, dto.CategoriaId);
            db.Menus.Add(menu);
            await db.SaveChangesAsync();

            return Results.Created($"/api/menus/{menu.Id}", new { menu.Id, mensaje = "Menú creado correctamente." });
        })
        .WithName("CrearMenu")
        .WithSummary("Crea un nuevo menú");

        // DELETE /api/menus/{id} — Desactivar menú (soft delete)
        group.MapDelete("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var menu = await db.Menus.FindAsync(id);
            if (menu is null) return Results.NotFound(new { mensaje = "Menú no encontrado." });

            menu.Desactivar();

            await db.SaveChangesAsync();
            return Results.Ok(new { mensaje = "Menú desactivado correctamente." });
        })
        .WithName("DesactivarMenu")
        .WithSummary("Desactiva un menú (soft delete)");
    }
}
