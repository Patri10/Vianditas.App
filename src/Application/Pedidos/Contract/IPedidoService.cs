namespace Vianditas.Application.Pedidos.Contract;
using Vianditas.Application.Pedidos.Presentation.DTOs;
using Vianditas.Application.Pedidos.Services.DTOs;
public interface IPedidoService
{
    
    public Task<PedidoResponseDTO> CreatePedido(CrearPedidoCommandDTO command);

    public Task<PedidoResponseDTO> GetPedidoById(Guid id);

    public Task<List<PedidoResponseDTO>> GetAllPedidos();


    public Task <PedidoResponseDTO> UpdatePedido(Guid id, UpdatePedidoCommandDTO command);


    public Task<List<PedidoResponseDTO>> FindPedidosByUsuarioId(Guid usuarioId);

    public Task<PedidoResponseDTO> DeletePedido(Guid id);

}
