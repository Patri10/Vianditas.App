using Vianditas.Application.Pedidos.Presentation.DTOs;
using Vianditas.Application.Pedidos.Services.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Pedidos.Services;

public interface IPedidoService
{
    Task<PedidoResponseDTO?> CrearPedidoAsync(CrearPedidoRequestDTO dto);
    Task<PedidoResponseDTO?> ObtenerPorIdAsync(Guid id);
}

public class PedidoService : IPedidoService
{
    private readonly ViandistasDbContext _dbContext;

    public PedidoService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PedidoResponseDTO?> CrearPedidoAsync(CrearPedidoRequestDTO dto)
    {
        // TODO: Implementar lógica. Regla: Crear pedido con estado Pendiente
        throw new NotImplementedException();
    }

    public async Task<PedidoResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
