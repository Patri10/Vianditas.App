using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usuarios").WithTags("Usuarios");

        // GET /api/usuarios
        group.MapGet("/", async (ViandistasDbContext db) =>
        {
            var usuarios = await db.Usuarios
                .Select(u => new UsuarioDto(u.Id, u.Nombre, u.Correo))
                .ToListAsync();
            return Results.Ok(usuarios);
        })
        .WithName("GetUsuarios")
        .WithSummary("Obtiene todos los usuarios");

        // GET /api/usuarios/{id}
        group.MapGet("/{id:guid}", async (Guid id, ViandistasDbContext db) =>
        {
            var usuario = await db.Usuarios.FindAsync(id);
            if (usuario is null) return Results.NotFound(new { mensaje = "Usuario no encontrado." });

            return Results.Ok(new UsuarioDto(usuario.Id, usuario.Nombre, usuario.Correo));
        })
        .WithName("GetUsuarioById")
        .WithSummary("Obtiene un usuario por ID");

        // GET /api/usuarios/buscar?correo=... — Buscar por correo (para el bot)
        group.MapGet("/buscar", async (string correo, ViandistasDbContext db) =>
        {
            var usuario = await db.Usuarios
                .Where(u => u.Correo == correo)
                .Select(u => new UsuarioDto(u.Id, u.Nombre, u.Correo))
                .FirstOrDefaultAsync();

            if (usuario is null) return Results.NotFound(new { mensaje = "Usuario no encontrado." });
            return Results.Ok(usuario);
        })
        .WithName("BuscarUsuarioPorCorreo")
        .WithSummary("Busca un usuario por correo electrónico")
        .WithDescription("Usado por el bot para verificar si el cliente ya está registrado.");

        // POST /api/usuarios — Registrar nuevo usuario
        group.MapPost("/", async (CrearUsuarioDto dto, ViandistasDbContext db) =>
        {
            var existe = await db.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
            if (existe) return Results.Conflict(new { mensaje = "Ya existe un usuario con ese correo." });

            var usuario = new Usuarios(dto.Nombre, dto.Correo, dto.Contrasena);
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();

            return Results.Created($"/api/usuarios/{usuario.Id}",
                new UsuarioDto(usuario.Id, usuario.Nombre, usuario.Correo));
        })
        .WithName("CrearUsuario")
        .WithSummary("Registra un nuevo usuario")
        .WithDescription("El bot llama a este endpoint cuando un nuevo cliente quiere registrarse.");
    }
}
