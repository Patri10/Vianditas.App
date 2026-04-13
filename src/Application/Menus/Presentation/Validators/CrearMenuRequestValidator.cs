using Vianditas.Application.Menus.Presentation.DTOs;
using FluentValidation;
namespace Vianditas.Application.Menus.Presentation.Validators;

public class CrearMenuRequestValidator : AbstractValidator<CrearMenuRequest>
{
    public CrearMenuRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del plato es requerido.")
            .MaximumLength(100).WithMessage("El nombre del plato no puede exceder los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción del plato no puede exceder los 500 caracteres.");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio del plato debe ser mayor que cero.");

        RuleFor(x => x.ComercioId)
            .NotEmpty().WithMessage("El ID del comercio es requerido.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage("El ID de la categoría es requerido.");
    }
    
}