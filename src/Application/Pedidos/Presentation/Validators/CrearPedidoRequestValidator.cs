using FluentValidation;
using Vianditas.Application.Pedidos.Presentation.DTOs;

namespace Vianditas.Application.Pedidos.Presentation.Validators;

public class CrearPedidoRequestValidator : AbstractValidator<CrearPedidoRequestDTO>
{
    public CrearPedidoRequestValidator()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty()
            .WithMessage("El usuario es obligatorio");

        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("La categoría es obligatoria");

        RuleFor(x => x.Detalles)
            .NotEmpty()
            .WithMessage("El pedido debe contener al menos un detalle")
            .Must(x => x.Count > 0)
            .WithMessage("Debe agregar al menos un plato al pedido");

        RuleForEach(x => x.Detalles)
            .SetValidator(new DetallePedidoRequestValidator());

        RuleFor(x => x.Notas)
            .MaximumLength(500)
            .WithMessage("Las notas no pueden exceder 500 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Notas));
    }
}

public class DetallePedidoRequestValidator : AbstractValidator<DetallePedidoRequestDTO>
{
    public DetallePedidoRequestValidator()
    {
        RuleFor(x => x.MenuId)
            .NotEmpty()
            .WithMessage("El plato es obligatorio");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor a 0")
            .LessThanOrEqualTo(100)
            .WithMessage("No se pueden pedir más de 100 unidades por plato");

        RuleFor(x => x.PrecioUnitario)
            .GreaterThan(0)
            .WithMessage("El precio unitario debe ser mayor a 0");
    }
}
