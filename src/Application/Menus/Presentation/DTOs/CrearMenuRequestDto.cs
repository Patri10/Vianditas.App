namespace Vianditas.Application.Menus.Presentation.DTOs;

public class CrearMenuRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;
    public Guid ComercioId { get; set; }
    public Guid CategoriaId { get; set; }

}
