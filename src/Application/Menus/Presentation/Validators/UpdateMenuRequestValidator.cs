namespace Vianditas.Application.Menus.Presentation.Validators;

using FluentValidation;
using Vianditas.Application.Menus.Presentation.DTOs;

public class UpdateMenuRequestValidator : AbstractValidator<PutUpdateCommandDTO>
{
    public UpdateMenuRequestValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre del menú es requerido.");
        RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio del menú debe ser mayor que cero.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción del menú es requerida.");
    }
}