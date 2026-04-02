using Vianditas.Application.Usuarios.Presentation.DTOs;
using Vianditas.Application.Usuarios.Services.DTOs;

namespace Vianditas.Application.Usuarios.Contract;

public interface IUsuarioService
{
    // Commands
    Task<UsuarioResponseDTO> CreateUserAsync(CreateUserCommandDTO command);
    Task<UsuarioResponseDTO> UpdateUserAsync(Guid id, UpdateUserCommandDTO command);
    Task DeleteUserAsync(Guid id, DeleteUserCommandDTO command);

    // Queries
    Task<UsuarioResponseDTO?> FindByIdAsync(Guid id);
    Task<List<UsuarioResponseDTO>> FindByNameAsync(string nombre);
    Task<List<UsuarioResponseDTO>> GetAllAsync();
}
