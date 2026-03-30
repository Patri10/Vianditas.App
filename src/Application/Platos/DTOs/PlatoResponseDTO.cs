namespace Vianditas.Application.Platos.DTOs;

public class PlatoResponseDTO
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string? Descripcion { get; set; }
    public Guid CategoriaId { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;
    public Guid ComercioId { get; set; }
    public bool Activo { get; set; }
}
