namespace Vianditas.Application.Menus.Presentation.Validators;
using FluentValidation;
using Vianditas.Application.Menus.Presentation.DTOs;

public class DeleteUsuarioRequestValidator : AbstractValidator<DeleteUsuarioRequestDTO>
{
    public DeleteUsuarioRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El ID del usuario es requerido.")
            .Must(id => id != Guid.Empty).WithMessage("El ID del usuario debe ser un GUID válido.");
    }
}