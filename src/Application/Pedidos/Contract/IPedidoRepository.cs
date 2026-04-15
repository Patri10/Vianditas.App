namespace Vianditas.Application.Pedidos.Contract;
using PedidoEntity = Vianditas.Domain.model.Pedido;

public interface IPedidoRepository
{
    public Task<PedidoEntity> GetByIdAsync(Guid id);
    public Task<List<PedidoEntity>> GetAllAsync();
    public Task AddAsync(PedidoEntity pedido);
    public Task UpdateAsync(PedidoEntity pedido);
    public Task DeleteAsync(Guid id);
    
}
