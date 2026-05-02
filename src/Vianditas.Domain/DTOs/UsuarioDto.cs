namespace Vianditas.Domain.DTOs;

public record UsuarioDto(
    Guid Id,
    string Nombre,
    string Correo
);

public record CrearUsuarioDto(
    string Nombre,
    string Correo,
    string Contrasena
);

// DTO especial para el bot de WhatsApp — busca o crea usuario por teléfono/nombre
public record BotRegistroDto(
    string Nombre,
    string Correo,
    string Contrasena
);
