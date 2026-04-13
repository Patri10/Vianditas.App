namespace Vianditas.Application.Menus.Contract;

using Vianditas.Application.Menus.Services.DTOs;
using Vianditas.Application.Menus.Presentation.DTOs;

public interface IMenuService
{
    Task<MenuResponseDTO> CreateMenu(CreateMenuCommandDTO command);
    Task<MenuResponseDTO> PutUpdateMenu(Guid id, PutUpdateCommandDTO command);
    Task<MenuResponseDTO> PathUpdateMenu(Guid id, PatchCommandDTO command);
    Task DeleteMenu(Guid id, DeleteMenuCommandDTO command);
    Task<MenuResponseDTO> GetByIdAsync(Guid id);
    Task<List<MenuResponseDTO>> FindByName(string nombre);
    Task<List<MenuResponseDTO>> GetAllAsync();


}
