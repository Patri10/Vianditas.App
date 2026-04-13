namespace Vianditas.Application.Menus.Presentation.DTOs;

public class PutUpdateCommandDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; }
}
