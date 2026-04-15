using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Vianditas.Application.Pedidos.Contract;
using Vianditas.Application.Pedidos.Presentation.DTOs;
using Vianditas.Application.Pedidos.Services.DTOs;
using Vianditas.Data;
using Vianditas.Domain.model;

namespace Vianditas.Application.Pedidos.Services;

public class PedidoService : IPedidoService
{
    private static readonly Dictionary<EstadoPedido, EstadoPedido[]> AllowedTransitions = new()
    {
        [EstadoPedido.Pendiente] = new[] { EstadoPedido.ListoParaEntrega, EstadoPedido.Cancelado },
        [EstadoPedido.ListoParaEntrega] = new[] { EstadoPedido.Entregado, EstadoPedido.Cancelado },
        [EstadoPedido.Entregado] = Array.Empty<EstadoPedido>(),
        [EstadoPedido.Cancelado] = Array.Empty<EstadoPedido>()
    };

    private readonly IPedidoRepository _pedidoRepository;
    private readonly ViandistasDbContext _dbContext;

    public PedidoService(IPedidoRepository pedidoRepository, ViandistasDbContext dbContext)
    {
        _pedidoRepository = pedidoRepository;
        _dbContext = dbContext;
    }

    public async Task<PedidoResponseDTO> CreatePedido(CrearPedidoCommandDTO command)
    {
        await ValidateCreateCommandAsync(command);

        var detallesJson = JsonSerializer.Serialize(command.Detalles);
        var pedido = new Pedido(command.Estado, command.UsuarioId, command.CategoriaId, detallesJson, command.Total, command.HoraCreacion);

        await _pedidoRepository.AddAsync(pedido);
        await ReplaceDetallesPedidoAsync(pedido.Id, command.Detalles);

        return await MapToResponseAsync(pedido);
    }

    public async Task<PedidoResponseDTO> GetPedidoById(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        return await MapToResponseAsync(pedido);
    }

    public async Task<List<PedidoResponseDTO>> GetAllPedidos()
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        var response = new List<PedidoResponseDTO>(pedidos.Count);

        foreach (var pedido in pedidos)
        {
            response.Add(await MapToResponseAsync(pedido));
        }

        return response;
    }

    public async Task<PedidoResponseDTO> UpdatePedido(Guid id, UpdatePedidoCommandDTO command)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        await ValidateUpdateCommandAsync(pedido, command);

        var detallesToPersist = command.Detalles;
        if (detallesToPersist is null)
        {
            detallesToPersist = await GetCurrentDetallesAsCommandAsync(pedido);
        }

        var newTotal = command.Total ?? CalculateTotal(detallesToPersist);
        var detallesJson = JsonSerializer.Serialize(detallesToPersist);

        pedido.Update(command.Estado, detallesJson, newTotal);
        await _pedidoRepository.UpdateAsync(pedido);

        if (command.Detalles is not null)
        {
            await ReplaceDetallesPedidoAsync(pedido.Id, command.Detalles);
        }

        return await MapToResponseAsync(pedido);
    }

    public async Task<List<PedidoResponseDTO>> FindPedidosByUsuarioId(Guid usuarioId)
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        var pedidosUsuario = pedidos.Where(p => p.UsuarioId == usuarioId).ToList();

        if (pedidosUsuario.Count == 0)
        {
            throw new KeyNotFoundException("No se encontraron pedidos para el usuario especificado.");
        }

        var response = new List<PedidoResponseDTO>(pedidosUsuario.Count);
        foreach (var pedido in pedidosUsuario)
        {
            response.Add(await MapToResponseAsync(pedido));
        }

        return response;
    }

    public async Task<PedidoResponseDTO> DeletePedido(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);
        var response = await MapToResponseAsync(pedido);

        await _pedidoRepository.DeleteAsync(id);
        return response;
    }

    private async Task ValidateCreateCommandAsync(CrearPedidoCommandDTO command)
    {
        if (command.Detalles is null || command.Detalles.Count == 0)
        {
            throw new ArgumentException("El pedido debe contener al menos un detalle.");
        }

        ValidateTotalConsistency(command.Total, command.Detalles, "El total no coincide con la suma de los detalles.");
        await ValidateEntitiesExistAsync(command.UsuarioId, command.CategoriaId, command.Detalles.Select(d => d.MenuId).ToList());
    }

    private async Task ValidateUpdateCommandAsync(Pedido pedido, UpdatePedidoCommandDTO command)
    {
        if (command.Estado.HasValue)
        {
            ValidateTransition(pedido.Estado, command.Estado.Value);
        }

        if (command.Detalles is not null)
        {
            if (command.Detalles.Count == 0)
            {
                throw new ArgumentException("Si envias detalles en una actualizacion, debe incluir al menos un item.");
            }

            await ValidateMenuIdsExistAsync(command.Detalles.Select(d => d.MenuId).ToList());

            if (command.Total.HasValue)
            {
                ValidateTotalConsistency(command.Total.Value, command.Detalles, "El total actualizado no coincide con la suma de los detalles.");
            }
        }
        else if (command.Total.HasValue)
        {
            var currentDetalles = await GetCurrentDetallesAsCommandAsync(pedido);
            ValidateTotalConsistency(command.Total.Value, currentDetalles, "No puedes actualizar el total con un valor inconsistente con los detalles actuales.");
        }
    }

    private static decimal CalculateTotal(List<DetallePedidoCommandDTO> detalles)
    {
        return detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
    }

    private static void ValidateTotalConsistency(decimal total, List<DetallePedidoCommandDTO> detalles, string message)
    {
        var expected = CalculateTotal(detalles);
        if (Math.Abs(total - expected) > 0.01m)
        {
            throw new ArgumentException(message);
        }
    }

    private static void ValidateTransition(EstadoPedido current, EstadoPedido next)
    {
        if (current == next)
        {
            return;
        }

        if (!AllowedTransitions.TryGetValue(current, out var allowed) || !allowed.Contains(next))
        {
            throw new InvalidOperationException($"Transicion de estado invalida: {current} -> {next}.");
        }
    }

    private async Task ValidateEntitiesExistAsync(Guid usuarioId, Guid categoriaId, List<Guid> menuIds)
    {
        var usuarioExists = await _dbContext.Usuarios.AnyAsync(u => u.Id == usuarioId);
        if (!usuarioExists)
        {
            throw new KeyNotFoundException("El usuario no existe.");
        }

        var categoriaExists = await _dbContext.Categorias.AnyAsync(c => c.Id == categoriaId);
        if (!categoriaExists)
        {
            throw new KeyNotFoundException("La categoria no existe.");
        }

        await ValidateMenuIdsExistAsync(menuIds);
    }

    private async Task ValidateMenuIdsExistAsync(List<Guid> menuIds)
    {
        var uniqueMenuIds = menuIds.Distinct().ToList();
        var existingIds = await _dbContext.Menus
            .Where(m => uniqueMenuIds.Contains(m.Id))
            .Select(m => m.Id)
            .ToListAsync();

        var missingIds = uniqueMenuIds.Except(existingIds).ToList();
        if (missingIds.Count > 0)
        {
            throw new KeyNotFoundException($"No existen menus para los ids: {string.Join(", ", missingIds)}");
        }
    }

    private async Task ReplaceDetallesPedidoAsync(Guid pedidoId, List<DetallePedidoCommandDTO> detalles)
    {
        var existingDetalles = await _dbContext.DetallePedidos
            .Where(d => d.PedidoId == pedidoId)
            .ToListAsync();

        if (existingDetalles.Count > 0)
        {
            _dbContext.DetallePedidos.RemoveRange(existingDetalles);
        }

        var newDetalles = detalles
            .Select(d => new Detalle_Pedido(d.Cantidad, Convert.ToDouble(d.PrecioUnitario), d.MenuId, pedidoId))
            .ToList();

        await _dbContext.DetallePedidos.AddRangeAsync(newDetalles);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<List<DetallePedidoCommandDTO>> GetCurrentDetallesAsCommandAsync(Pedido pedido)
    {
        var detallesDb = await _dbContext.DetallePedidos
            .Where(d => d.PedidoId == pedido.Id)
            .Select(d => new DetallePedidoCommandDTO
            {
                MenuId = d.MenuId,
                Cantidad = d.Cantidad,
                PrecioUnitario = Convert.ToDecimal(d.PrecioUnitario)
            })
            .ToListAsync();

        if (detallesDb.Count > 0)
        {
            return detallesDb;
        }

        if (string.IsNullOrWhiteSpace(pedido.Detalles))
        {
            return new List<DetallePedidoCommandDTO>();
        }

        return JsonSerializer.Deserialize<List<DetallePedidoCommandDTO>>(pedido.Detalles)
            ?? new List<DetallePedidoCommandDTO>();
    }

    private async Task<PedidoResponseDTO> MapToResponseAsync(Pedido pedido)
    {
        var detalles = await _dbContext.DetallePedidos
            .Where(d => d.PedidoId == pedido.Id)
            .Join(
                _dbContext.Menus,
                detalle => detalle.MenuId,
                menu => menu.Id,
                (detalle, menu) => new DetallePedidoResponseDTO
                {
                    MenuId = detalle.MenuId,
                    PlatoNombre = menu.Nombre,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = Convert.ToDecimal(detalle.PrecioUnitario),
                    Subtotal = detalle.Cantidad * Convert.ToDecimal(detalle.PrecioUnitario)
                })
            .ToListAsync();

        if (detalles.Count == 0 && !string.IsNullOrWhiteSpace(pedido.Detalles))
        {
            var detallesJson = JsonSerializer.Deserialize<List<DetallePedidoCommandDTO>>(pedido.Detalles)
                ?? new List<DetallePedidoCommandDTO>();

            detalles = detallesJson.Select(d => new DetallePedidoResponseDTO
            {
                MenuId = d.MenuId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Cantidad * d.PrecioUnitario
            }).ToList();
        }

        return new PedidoResponseDTO
        {
            Id = pedido.Id,
            Estado = pedido.Estado,
            UsuarioId = pedido.UsuarioId,
            CategoriaId = pedido.CategoriaId,
            Detalles = detalles,
            Total = pedido.Total,
            HoraCreacion = pedido.HoraCreacion
        };
    }
}
