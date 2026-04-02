using Microsoft.EntityFrameworkCore;
using Vianditas.Application.Usuarios.Contract;
using Vianditas.Data;
using Vianditas.Domain.model;
using UsuarioEntity = Vianditas.Domain.model.Usuarios;

namespace Vianditas.Application.Usuarios.infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ViandistasDbContext _dbContext;

    public UsuarioRepository(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UsuarioEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UsuarioEntity?> GetByNumeroWhatsappAsync(string numeroWhatsapp)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NumeroWhatsapp == numeroWhatsapp);
    }

    public async Task<List<UsuarioEntity>> GetByNombreAsync(string nombre)
    {
        return await _dbContext.Usuarios
            .Where(u => u.Nombre.Contains(nombre))
            .ToListAsync();
    }

    public async Task<List<UsuarioEntity>> GetAllAsync()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task AddAsync(UsuarioEntity usuario)
    {
        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UsuarioEntity usuario)
    {
        _dbContext.Usuarios.Update(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _dbContext.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();
        }
    }
}
