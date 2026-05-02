namespace Vianditas.Domain.DTOs;

public record MenuDto(
    Guid Id,
    string Nombre,
    decimal Precio,
    string Descripcion,
    string CategoriaNombre,
    string ComercioNombre,
    bool Activo
);

public record CrearMenuDto(
    string Nombre,
    decimal Precio,
    string Descripcion,
    Guid ComercioId,
    Guid CategoriaId
);
