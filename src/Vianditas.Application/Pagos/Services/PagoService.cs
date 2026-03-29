using Vianditas.Application.Pagos.Presentation.DTOs;
using Vianditas.Application.Pagos.Services.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Pagos.Services;

public interface IPagoService
{
    Task<PagoResponseDTO?> CrearPagoAsync(CrearPagoRequestDTO dto);
    Task<PagoResponseDTO?> ObtenerPorIdAsync(Guid id);
}

public class PagoService : IPagoService
{
    private readonly ViandistasDbContext _dbContext;

    public PagoService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagoResponseDTO?> CrearPagoAsync(CrearPagoRequestDTO dto)
    {
        // TODO: Implementar lógica + integración MercadoPago
        throw new NotImplementedException();
    }

    public async Task<PagoResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
