using Vianditas.Domain.model;
using UsuarioEntity = Vianditas.Domain.model.Usuarios;

namespace Vianditas.Application.Usuarios.Contract;

public interface IUsuarioRepository
{
    Task<UsuarioEntity?> GetByIdAsync(Guid id);
    Task<UsuarioEntity?> GetByNumeroWhatsappAsync(string numeroWhatsapp);
    Task<List<UsuarioEntity>> GetByNombreAsync(string nombre);
    Task<List<UsuarioEntity>> GetAllAsync();
    Task AddAsync(UsuarioEntity usuario);
    Task UpdateAsync(UsuarioEntity usuario);
    Task DeleteAsync(Guid id);
}
