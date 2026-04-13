namespace Vianditas.Application.Menus.Services.DTOs;

public class CreateMenuCommandDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public Guid CategoriaId { get; set; } = Guid.Empty;

    public Guid ComercioId { get; set; } = Guid.Empty;
}