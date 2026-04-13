namespace Vianditas.Application.Menus.Presentation.DTOs;
using Vianditas.Domain.model;
public class PutUpdateRequestDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; }

    
}