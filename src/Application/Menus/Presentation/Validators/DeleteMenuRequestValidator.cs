namespace Vianditas.Application.Menus.Presentation.Validators;

using FluentValidation;
using Vianditas.Application.Menus.Presentation.DTOs;

public class DeleteMenuRequestValidator : AbstractValidator<DeleteMenuRequestDTO>
{
    public DeleteMenuRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El ID del menú es requerido.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("El ID del menú debe ser un GUID válido.");
    }
}