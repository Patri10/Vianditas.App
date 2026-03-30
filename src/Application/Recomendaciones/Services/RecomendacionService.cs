using Vianditas.Application.Recomendaciones.Services.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Recomendaciones.Services;

public interface IRecomendacionService
{
    /// <summary>
    /// Obtiene 3 platos recomendados para una categoría en la fecha actual.
    /// Regla de negocio: El sistema devuelve 3 platos:
    /// - El más caro (calidad premium)
    /// - El más barato (economía)
    /// - Uno aleatorio
    /// Filtrados por CategoriaId y DisponibilidadDiaria (disponible hoy)
    /// </summary>
    Task<List<RecomendacionResponseDTO>> ObtenerRecomendacionesAsync(Guid categoriaId);
}

public class RecomendacionService : IRecomendacionService
{
    private readonly ViandistasDbContext _dbContext;

    public RecomendacionService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RecomendacionResponseDTO>> ObtenerRecomendacionesAsync(Guid categoriaId)
    {
        // TODO: Implementar lógica
        // 1. Obtener fecha actual (solo date, sin time)
        // 2. Filtrar Menu por CategoriaId + Activo = true
        // 3. Filtrar por DisponibilidadDiaria donde Fecha = hoy y Disponible = true
        // 4. Si hay 3+ platos: más caro + más barato + random
        // 5. Si hay <3 platos: devolver los que haya ordenados por precio
        throw new NotImplementedException();
    }
}
