using Vianditas.Application.Pedidos.Contract;
using Microsoft.EntityFrameworkCore;
using Vianditas.Domain.model;
using Vianditas.Data;
using Pedido = Vianditas.Domain.model.Pedido;

using Vianditas.Application.Pedidos.Presentation.DTOs;

namespace Vianditas.Application.Pedidos.infrastructure;

public class PedidoRepository : IPedidoRepository
{
    private readonly ViandistasDbContext _dbContext;

    public PedidoRepository(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task AddAsync(Pedido pedido)
    {
        _dbContext.Pedidos.Add(pedido);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var pedido = await _dbContext.Pedidos.FindAsync(id);
        if (pedido != null)
        {
            _dbContext.Pedidos.Remove(pedido);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Pedido pedido)
    {
        _dbContext.Pedidos.Update(pedido);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Pedido>> GetAllAsync()
    {
        return await _dbContext.Pedidos.ToListAsync();
    }

    public async Task<Pedido> GetByIdAsync(Guid id)
    {
        var pedido = await _dbContext.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null)
        {
            throw new KeyNotFoundException("Pedido no encontrado");
        }
        return pedido;
    }


}
