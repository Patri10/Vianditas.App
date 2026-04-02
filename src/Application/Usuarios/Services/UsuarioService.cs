using Vianditas.Application.Usuarios.Contract;
using Vianditas.Application.Usuarios.Presentation.DTOs;
using Vianditas.Application.Usuarios.Services.DTOs;
using Vianditas.Domain.model;
using UsuarioEntity = Vianditas.Domain.model.Usuarios;

namespace Vianditas.Application.Usuarios.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioResponseDTO> CreateUserAsync(CreateUserCommandDTO command)
    {
        var existingUser = await _repository.GetByNumeroWhatsappAsync(command.NumeroWhatsapp);

        if (existingUser != null)
        {
            throw new InvalidOperationException("Ya existe un usuario con ese número de WhatsApp.");
        }

        var usuario = new UsuarioEntity(command.Nombre, command.NumeroWhatsapp, command.WhatsappUserId);

        await _repository.AddAsync(usuario);

        return MapToResponse(usuario);
    }
    public async Task<UsuarioResponseDTO?> FindByIdAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        return usuario == null ? null : MapToResponse(usuario);
    }

    public async Task<List<UsuarioResponseDTO>> FindByNameAsync(string nombre)
    {
        var usuarios = await _repository.GetByNombreAsync(nombre);
        return usuarios.Select(MapToResponse).ToList();
    }

    public async Task<UsuarioResponseDTO> UpdateUserAsync(Guid id, UpdateUserCommandDTO command)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuario no encontrado.");
        }

        usuario.Update(command.Nombre, command.NumeroWhatsapp, command.WhatsappUserId);
        await _repository.UpdateAsync(usuario);

        return MapToResponse(usuario);
    }

    public async Task DeleteUserAsync(Guid id, DeleteUserCommandDTO command)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<List<UsuarioResponseDTO>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(MapToResponse).ToList();
    }

    private UsuarioResponseDTO MapToResponse(UsuarioEntity usuario)
    {
        return new UsuarioResponseDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            NumeroWhatsapp = usuario.NumeroWhatsapp,
            WhatsappUserId = usuario.WhatsappUserId
        };
    }
}
