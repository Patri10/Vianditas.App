using Vianditas.Application.Platos.Presentation.DTOs;
using Vianditas.Domain.model;
using Vianditas.Data;

namespace Vianditas.Application.Platos.Services;

public interface IPlatoService
{
    Task<PlatoResponseDTO?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<PlatoResponseDTO>> ObtenerPorComercioAsync(Guid comercioId);
}

public class PlatoService : IPlatoService
{
    private readonly ViandistasDbContext _dbContext;

    public PlatoService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PlatoResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PlatoResponseDTO>> ObtenerPorComercioAsync(Guid comercioId)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
