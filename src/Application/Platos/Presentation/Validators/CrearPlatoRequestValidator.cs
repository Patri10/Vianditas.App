using FluentValidation;
using Vianditas.Application.Platos.Presentation.DTOs;

namespace Vianditas.Application.Platos.Presentation.Validators;

public class CrearPlatoRequestValidator : AbstractValidator<CrearPlatoRequestDTO>
{
    public CrearPlatoRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre del plato es obligatorio")
            .Length(3, 100)
            .WithMessage("El nombre debe tener entre 3 y 100 caracteres")
            .Matches(@"^[a-zA-Z0-9\sáéíóúñ]+$")
            .WithMessage("Solo se permiten letras, números, espacios y acentos");

        RuleFor(x => x.Precio)
            .GreaterThan(0)
            .WithMessage("El precio debe ser mayor a 0")
            .LessThanOrEqualTo(100000)
            .WithMessage("El precio no puede exceder 100.000");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500)
            .WithMessage("La descripción no puede exceder 500 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Descripcion));

        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("La categoría es obligatoria");

        RuleFor(x => x.ComercioId)
            .NotEmpty()
            .WithMessage("El comercio es obligatorio");
    }
}
