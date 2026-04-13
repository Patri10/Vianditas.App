using Microsoft.AspNetCore.Mvc;
using Vianditas.Application.Usuarios.Contract;
using Vianditas.Application.Usuarios.Presentation.DTOs;
using Vianditas.Application.Usuarios.Services.DTOs;

namespace Vianditas.Application.Usuarios.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioResponseDTO>>> GetAll()
    {
        var usuarios = await _usuarioService.GetAll();
        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsuarioResponseDTO>> GetById(Guid id)
    {
        var usuario = await _usuarioService.FindById(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpGet("by-name/{nombre}")]
    public async Task<ActionResult<List<UsuarioResponseDTO>>> GetByName(string nombre)
    {
        var usuarios = await _usuarioService.FindByName(nombre);
        return Ok(usuarios);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDTO>> Create([FromBody] CrearUsuarioRequestDTO request)
    {
        var command = new CreateUserCommandDTO
        {
            Nombre = request.Nombre,
            NumeroWhatsapp = request.NumeroWhatsapp,
            WhatsappUserId = request.WhatsappUserId
        };

        var usuario = await _usuarioService.CreateUser(command);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UsuarioResponseDTO>> Update(Guid id, [FromBody] UpdateUserRequestDTO request)
    {
        var command = new UpdateUserCommandDTO
        {
            Id = id,
            Nombre = request.Nombre,
            NumeroWhatsapp = request.NumeroWhatsapp,
            WhatsappUserId = request.WhatsappUserId
        };

        var usuario = await _usuarioService.UpdateUser(id, command);
        return Ok(usuario);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommandDTO
        {
            Id = id,
            Nombre = string.Empty,
            NumeroWhatsapp = string.Empty,
            WhatsappUserId = string.Empty
        };

        await _usuarioService.DeleteUser(id, command);
        return NoContent();
    }
}
