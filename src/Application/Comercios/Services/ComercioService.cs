using Vianditas.Application.Comercios.Presentation.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Comercios.Services;

public interface IComercioService
{
    Task<IEnumerable<ComercioResponseDTO>> ObtenerTodosAsync();
    Task<ComercioResponseDTO?> ObtenerPorIdAsync(Guid id);
}

public class ComercioService : IComercioService
{
    private readonly ViandistasDbContext _dbContext;

    public ComercioService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ComercioResponseDTO>> ObtenerTodosAsync()
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }

    public async Task<ComercioResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
