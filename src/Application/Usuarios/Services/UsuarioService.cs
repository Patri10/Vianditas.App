using Vianditas.Application.Usuarios.Presentation.DTOs;
using Vianditas.Application.Usuarios.Services.DTOs;
using Vianditas.Data;

namespace Vianditas.Application.Usuarios.Services;

public interface IUsuarioService
{
    Task<UsuarioResponseDTO?> CrearUsuarioAsync(CrearUsuarioRequestDTO dto);
    Task<UsuarioResponseDTO?> ObtenerPorIdAsync(Guid id);
}

public class UsuarioService : IUsuarioService
{
    private readonly ViandistasDbContext _dbContext;

    public UsuarioService(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UsuarioResponseDTO?> CrearUsuarioAsync(CrearUsuarioRequestDTO dto)
    {
        // TODO: Implementar logica de alta y sincronizacion de identidad WhatsApp
        throw new NotImplementedException();
    }

    public async Task<UsuarioResponseDTO?> ObtenerPorIdAsync(Guid id)
    {
        // TODO: Implementar lógica
        throw new NotImplementedException();
    }
}
