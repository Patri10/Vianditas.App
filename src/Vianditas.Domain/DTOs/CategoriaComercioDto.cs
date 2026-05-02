namespace Vianditas.Domain.DTOs;

public record CategoriaDto(
    Guid Id,
    string Nombre
);

public record CrearCategoriaDto(
    string Nombre
);

public record ComercioDto(
    Guid Id,
    string Nombre,
    string Direccion,
    string Telefono,
    bool Activo
);

public record CrearComercioDto(
    string Nombre,
    string Direccion,
    string Telefono
);
