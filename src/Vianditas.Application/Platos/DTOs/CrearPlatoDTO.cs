namespace Vianditas.Application.Platos.DTOs;

public class CrearPlatoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string? Descripcion { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid ComercioId { get; set; }
}
