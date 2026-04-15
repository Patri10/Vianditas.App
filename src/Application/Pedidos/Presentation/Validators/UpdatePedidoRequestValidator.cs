using FluentValidation;
using Vianditas.Application.Pedidos.Presentation.DTOs;

namespace Vianditas.Application.Pedidos.Presentation.Validators;

public class UpdatePedidoRequestValidator : AbstractValidator<UpdatePedidoRequestDTO>
{
    public UpdatePedidoRequestValidator()
    {
        RuleFor(x => x.Estado)
            .IsInEnum()
            .When(x => x.Estado.HasValue)
            .WithMessage("El estado del pedido no es valido.");

        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Total.HasValue)
            .WithMessage("El total debe ser mayor o igual a 0.");

        RuleFor(x => x.Detalles)
            .Must(detalles => detalles == null || detalles.Count > 0)
            .WithMessage("Si envias detalles, debe incluir al menos un item.");

        RuleForEach(x => x.Detalles)
            .SetValidator(new DetallePedidoRequestValidator())
            .When(x => x.Detalles != null);
    }
}