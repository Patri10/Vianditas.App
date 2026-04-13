using Microsoft.AspNetCore.Mvc;
using Vianditas.Application.Menus.Contract;
using Vianditas.Application.Menus.Presentation.DTOs;
using Vianditas.Application.Menus.Services.DTOs;

namespace Vianditas.Application.Menus.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenusController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenusController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet("by-name/{nombre}")]
    public async Task<ActionResult<List<MenuResponseDTO>>> GetByName(string nombre)
    {
        var menus = await _menuService.FindByName(nombre);
        return Ok(menus);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MenuResponseDTO>> GetById(Guid id)
    {
        var menu = await _menuService.GetByIdAsync(id);
        return Ok(menu);
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuResponseDTO>>> GetAll()
    {
        var menus = await _menuService.GetAllAsync();
        return Ok(menus);
    }



    [HttpPost]
    public async Task<ActionResult<MenuResponseDTO>> CreateMenu([FromBody] CrearMenuRequest request)
    {
        var command = new CreateMenuCommandDTO
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            CategoriaId = request.CategoriaId,
            ComercioId = request.ComercioId,
            Activo = request.Activo
        };

        var menu = await _menuService.CreateMenu(command);
        return CreatedAtAction(nameof(GetByName), new { nombre = menu.Nombre }, menu);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MenuResponseDTO>> PutUpdateMenu(Guid id, [FromBody] PutUpdateRequestDTO request)
    {
        var command = new PutUpdateCommandDTO
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            Activo = request.Activo
        };

        var menu = await _menuService.PutUpdateMenu(id, command);
        if (menu == null)
        {
            return NotFound("Menu no encontrado");
        }

        return Ok(menu);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<MenuResponseDTO>> PathUpdateMenu(Guid id, [FromBody] PatchUpdateRequestDto request)
    {
        var command = new PatchCommandDTO
        {
            Id = id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            Activo = request.Activo
        };

        var menu = await _menuService.PathUpdateMenu(id, command);
        if (menu == null)
        {
            return NotFound("Menu no encontrado");
        }

        return Ok(menu);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var command = new DeleteMenuCommandDTO
        {
            Id = id
        };

        await _menuService.DeleteMenu(id, command);
        return NoContent();
    }


}
