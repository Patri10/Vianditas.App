using Microsoft.EntityFrameworkCore;
using Vianditas.Application.Usuarios.Contract;
using Vianditas.Data;
using Vianditas.Domain.model; 
using Usuario = Vianditas.Domain.model.Usuarios;

namespace Vianditas.Application.Usuarios.infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ViandistasDbContext _dbContext;

    public UsuarioRepository(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    } 

    public async Task<Usuario?> GetByNumeroWhatsappAsync(string numeroWhatsapp)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NumeroWhatsapp == numeroWhatsapp);
    }

    public async Task<List<Usuario>> GetByNombreAsync(string nombre)
    {
        return await _dbContext.Usuarios
            .Where(u => u.Nombre.Contains(nombre))
            .ToListAsync();
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task AddAsync(Usuario usuario)
    {
        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
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
