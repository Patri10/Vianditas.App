namespace Vianditas.Application.Menus.Presentation.DTOs;

using Vianditas.Domain.model;

public class PatchUpdateRequestDto
{
    public string? Nombre { get;  set; } = null!;
    public decimal? Precio { get;  set; } = null!;
    public string? Descripcion { get;  set; } = null!;

    public bool? Activo { get;  set; } = null!;



}