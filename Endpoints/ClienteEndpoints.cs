using Microsoft.EntityFrameworkCore;
using Vianditas.Data;
using Vianditas.Domain.DTOs;
using Vianditas.Domain.model;

namespace Vianditas.API.Endpoints
{
    public static class ClienteEndpoints
    {
        public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/clientes").WithTags("Clientes");

            // Buscar cliente por teléfono (ideal para el inicio del bot de WhatsApp)
            group.MapGet("/buscar", async (string telefono, ViandistasDbContext db) =>
            {
                var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Telefono == telefono);
                
                if (cliente is null) return Results.NotFound(new { mensaje = "Cliente no encontrado." });

                return Results.Ok(new 
                { 
                    cliente.Id, 
                    cliente.Nombre, 
                    cliente.Apellido, 
                    cliente.Telefono, 
                    cliente.Direccion 
                });
            }).WithSummary("Busca un cliente por su número de teléfono");

            // Crear nuevo cliente
            group.MapPost("/", async (CrearClienteDto dto, ViandistasDbContext db) =>
            {
                var existe = await db.Clientes.AnyAsync(c => c.Telefono == dto.Telefono);
                if (existe) return Results.BadRequest(new { mensaje = "Ya existe un cliente con ese teléfono." });

                var cliente = new Cliente(dto.Nombre, dto.Apellido, dto.Telefono, dto.Direccion);
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();

                return Results.Created($"/api/clientes/{cliente.Id}", new 
                { 
                    cliente.Id, 
                    cliente.Nombre, 
                    cliente.Apellido, 
                    cliente.Telefono, 
                    cliente.Direccion,
                    mensaje = "Cliente creado exitosamente."
                });
            }).WithSummary("Registra un nuevo cliente");

            // Actualizar datos del cliente (ej. si cambia su dirección)
            group.MapPut("/{id:guid}", async (Guid id, ActualizarClienteDto dto, ViandistasDbContext db) =>
            {
                var cliente = await db.Clientes.FindAsync(id);
                if (cliente is null) return Results.NotFound(new { mensaje = "Cliente no encontrado." });

                cliente.ActualizarDatos(dto.Nombre, dto.Apellido, dto.Direccion);
                await db.SaveChangesAsync();

                return Results.Ok(new { mensaje = "Cliente actualizado exitosamente." });
            }).WithSummary("Actualiza los datos de un cliente existente");
        }
    }
}
