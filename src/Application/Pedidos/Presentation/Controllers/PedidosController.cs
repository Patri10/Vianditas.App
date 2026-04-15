using Microsoft.AspNetCore.Mvc;
using Vianditas.Application.Pedidos.Contract;
using Vianditas.Application.Pedidos.Presentation.DTOs;
using Vianditas.Application.Pedidos.Services.DTOs;

namespace Vianditas.Application.Pedidos.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{

    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<ActionResult<PedidoResponseDTO>> CreatePedido([FromBody] CrearPedidoRequestDTO request)
    {
        var command = new CrearPedidoCommandDTO
        {
            Estado = request.Estado,
            UsuarioId = request.UsuarioId,
            CategoriaId = request.CategoriaId,
            Detalles = request.Detalles.Select(d => new DetallePedidoCommandDTO
            {
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                MenuId = d.MenuId
            }).ToList(),
            Total = request.Total,
            HoraCreacion = request.HoraCreacion
        };

        var pedido = await _pedidoService.CreatePedido(command);
        return Ok(pedido);

    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PedidoResponseDTO>> GetById(Guid id)
    {
        var pedido = await _pedidoService.GetPedidoById(id);
        return Ok(pedido);
    }

    [HttpGet]
    public async Task<ActionResult<List<PedidoResponseDTO>>> GetAll()
    {
        var pedidos = await _pedidoService.GetAllPedidos();
        return Ok(pedidos);

    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdatePedido(Guid id, [FromBody] UpdatePedidoRequestDTO request)
    {
        var existingPedido = await _pedidoService.GetPedidoById(id);
        if (existingPedido == null)
        {
            return NotFound();

        }
        var command = new UpdatePedidoCommandDTO
        {
            Estado = request.Estado,
            Total = request.Total,
            Detalles = request.Detalles?.Select(d => new DetallePedidoCommandDTO
            {
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                MenuId = d.MenuId
            }).ToList()
        };

        var updatedPedido = await _pedidoService.UpdatePedido(id, command);
        if (updatedPedido == null)
        {
            return NotFound();
        }
        return Ok(updatedPedido);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePedido(Guid id)
    {
        var existingPedido = await _pedidoService.GetPedidoById(id);
        if (existingPedido == null)
        {
            return NotFound();
        }
        await _pedidoService.DeletePedido(id);
        return NoContent();

    }

    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<ActionResult<List<PedidoResponseDTO>>> GetPedidosByUsuarioId(Guid usuarioId)
    {
        var pedidos = await _pedidoService.FindPedidosByUsuarioId(usuarioId);
        return Ok(pedidos);
    }

}
