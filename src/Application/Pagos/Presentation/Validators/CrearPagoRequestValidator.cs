using FluentValidation;
using Vianditas.Application.Pagos.Presentation.DTOs;

namespace Vianditas.Application.Pagos.Presentation.Validators;

public class CrearPagoRequestValidator : AbstractValidator<CrearPagoRequestDTO>
{
    public CrearPagoRequestValidator()
    {
        RuleFor(x => x.PedidoId)
            .NotEmpty()
            .WithMessage("El pedido es obligatorio");

        RuleFor(x => x.Monto)
            .GreaterThan(0)
            .WithMessage("El monto debe ser mayor a 0")
            .LessThanOrEqualTo(1000000)
            .WithMessage("El monto no puede exceder 1.000.000");
    }
}
