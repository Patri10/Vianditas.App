namespace Vianditas.Application.Menus.Contract;
using MenuEntity = Vianditas.Domain.model.Menu;

public interface IMenuRepository
{
    public Task<MenuEntity?> GetByIdAsync(Guid id);
    public Task<List<MenuEntity>> GetAllAsync();
    public Task AddAsync(MenuEntity menu);
    public Task UpdateAsync(MenuEntity menu);
    public Task DeleteAsync(Guid id);

    public Task<List<MenuEntity>> GetByNombreAsync(string nombre);
}
