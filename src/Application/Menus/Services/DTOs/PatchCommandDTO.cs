namespace Vianditas.Application.Menus.Services.DTOs;

using Vianditas.Domain.model;

public class PatchCommandDTO
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; } = string.Empty;
    public decimal? Precio { get; set; }
    public string? Descripcion { get; set; } = string.Empty;
    public bool? Activo { get; set; }
    
}