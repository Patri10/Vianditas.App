using Vianditas.Application.Usuarios.Presentation.DTOs;
using Vianditas.Application.Usuarios.Services.DTOs;

namespace Vianditas.Application.Usuarios.Contract;

public interface IUsuarioService
{
    // Commands
    Task<UsuarioResponseDTO> CreateUser(CreateUserCommandDTO command);
    Task<UsuarioResponseDTO> UpdateUser(Guid id, UpdateUserCommandDTO command);
    Task DeleteUser(Guid id, DeleteUserCommandDTO command);

    // Queries
    Task<UsuarioResponseDTO?> FindById(Guid id);
    Task<List<UsuarioResponseDTO>> FindByName(string nombre);
    Task<List<UsuarioResponseDTO>> GetAll();
}
