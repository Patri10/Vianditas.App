using Vianditas.Application.Categorias.Presentation.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Categorias.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaResponseDTO>> ObtenerTodasAsync();
    Task<CategoriaResponseDTO?> ObtenerPorIdAsync(Guid id);
}

public class CategoriaService : ICategoriaService
{
    private readonly ViandistasDbContext _dbContext;

    public CategoriaService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CategoriaResponseDTO>> ObtenerTodasAsync()
    {
        
        throw new NotImplementedException();
    }

    public async Task<CategoriaResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
