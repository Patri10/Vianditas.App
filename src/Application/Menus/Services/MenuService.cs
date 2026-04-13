using Vianditas.Application.Categorias.Contract;
using Vianditas.Application.Menus.Contract;
using Vianditas.Application.Menus.Presentation.DTOs;
using Vianditas.Application.Menus.Services.DTOs;
using Menu = Vianditas.Domain.model.Menu;
namespace Vianditas.Application.Menus.Services;


public class MenuService : IMenuService
{

    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMenuRepository _menuRepository;
    public MenuService(IMenuRepository menuRepository, ICategoriaRepository categoriaRepository)
    {
        _menuRepository = menuRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<MenuResponseDTO> CreateMenu(CreateMenuCommandDTO command)
    {
        var existingMenu = await _menuRepository.GetByNombreAsync(command.Nombre);
        if (existingMenu != null)
        {
            throw new InvalidOperationException("Ya existe un menú con ese nombre.");
        }

        var existingCategoria = await _categoriaRepository.GetByIdAsync(command.CategoriaId);
        if (existingCategoria == null)
        {
            throw new KeyNotFoundException("Categoría no encontrada.");
        }

        var existingComercio = await _menuRepository.GetByIdAsync(command.ComercioId);
        if (existingComercio == null)
        {
            throw new KeyNotFoundException("Comercio no encontrado.");
        }

        var menu = new Menu(command.Nombre, command.Precio, command.Descripcion, command.CategoriaId, command.ComercioId);
        await _menuRepository.AddAsync(menu);

        return MapToResponse(menu);
    }

    public async Task<MenuResponseDTO> PutUpdateMenu(Guid id, PutUpdateCommandDTO command)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
        {
            throw new KeyNotFoundException("Menu no encontrado");
        }

        menu.Update(command.Nombre, command.Precio, command.Descripcion, command.Activo);
        await _menuRepository.UpdateAsync(menu);

        return MapToResponse(menu);
    }

    public async Task<MenuResponseDTO> PathUpdateMenu(Guid id, PatchCommandDTO command)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
        {
            throw new KeyNotFoundException("Menu no encontrado");
        }

        // Actualizar solo los campos que vienen en el comando
        if (!string.IsNullOrEmpty(command.Nombre))
        {
            menu.UpdateName(command.Nombre);
        }

        if (command.Precio.HasValue)
        {
            menu.UpdatePrice(command.Precio.Value);
        }

        if (!string.IsNullOrEmpty(command.Descripcion))
        {
            menu.UpdateDescription(command.Descripcion);
        }

        if (command.Activo.HasValue)
        {
            menu.Update(menu.Nombre, menu.Precio, menu.Descripcion, command.Activo.Value);
        }

        await _menuRepository.UpdateAsync(menu);
        return MapToResponse(menu);
    }

    public async Task DeleteMenu(Guid id, DeleteMenuCommandDTO command)
    {
        await _menuRepository.DeleteAsync(id);
    }

    public async Task<List<MenuResponseDTO>> FindByName(string nombre)
    {
        var menus = await _menuRepository.GetByNombreAsync(nombre);
        return menus.Select(MapToResponse).ToList();
    }

    public async Task<MenuResponseDTO> GetByIdAsync(Guid id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
        {
            throw new KeyNotFoundException("Menu no encontrado");
        }
        return MapToResponse(menu);
    }

    public async Task<List<MenuResponseDTO>> GetAllAsync()
    {
        var menus = await _menuRepository.GetAllAsync();
        return menus.Select(MapToResponse).ToList();
    }

    private MenuResponseDTO MapToResponse(Menu menu)
    {
        return new MenuResponseDTO
        {
            Id = menu.Id,
            Nombre = menu.Nombre,
            Descripcion = menu.Descripcion,
            Precio = menu.Precio
        };
    }


}
