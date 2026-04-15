using Microsoft.EntityFrameworkCore;
using Vianditas.Application.Menus.Contract;
using Vianditas.Application.Menus.Presentation.DTOs;
using Vianditas.Data;
using Vianditas.Domain.model;
using Menu = Vianditas.Domain.model.Menu;

 
namespace Vianditas.Application.Menus.infrastructure;

public class MenuRepository : IMenuRepository
{

    private readonly ViandistasDbContext _dbContext;

    public MenuRepository(ViandistasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Menu menu)
    {
        _dbContext.Menus.Add(menu);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var menu = await _dbContext.Menus.FindAsync(id);
        if (menu != null)
        {
            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Menu menu)
    {
        _dbContext.Menus.Update(menu);
        await _dbContext.SaveChangesAsync();
    }
           public async Task<List<Menu>> GetAllAsync ()
     {
         return await _dbContext.Menus.ToListAsync();
     }

    public async Task<Menu?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Menus.FirstOrDefaultAsync(m => m.Id == id); 

    }

    public async Task<List<Menu>> GetByNombreAsync(string nombre)
    {
        return await _dbContext.Menus
            .Where(m => m.Nombre.Contains(nombre))
            .ToListAsync();
    }

}
