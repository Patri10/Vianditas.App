namespace Vianditas.Domain.DTOs;

public record CrearClienteDto(string Nombre, string Apellido, string Telefono, string? Direccion);

public record ActualizarClienteDto(string Nombre, string Apellido, string? Direccion);
