namespace Vianditas.Application.Platos.Services.DTOs;

public class CrearPlatoCommandDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string? Descripcion { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid ComercioId { get; set; }
}
