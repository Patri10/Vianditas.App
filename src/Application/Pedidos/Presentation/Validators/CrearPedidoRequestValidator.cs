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
            .WithMessage("La categoria es obligatoria");

        RuleFor(x => x.Detalles)
            .NotEmpty()
            .WithMessage("El pedido debe contener al menos un detalle")
            .Must(x => x.Count > 0)
            .WithMessage("Debe agregar al menos un plato al pedido");

        RuleForEach(x => x.Detalles)
            .SetValidator(new DetallePedidoRequestValidator());

        RuleFor(x => x.Total)
            .GreaterThan(0)
            .WithMessage("El total del pedido debe ser mayor a 0");

        RuleFor(x => x)
            .Must(HaveConsistentTotal)
            .WithMessage("El total debe coincidir con la suma de subtotales de los detalles.");

        RuleFor(x => x.HoraCreacion)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("La hora de creacion no puede ser en el futuro");
    }

    private static bool HaveConsistentTotal(CrearPedidoRequestDTO pedido)
    {
        var expected = pedido.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
        return Math.Abs(pedido.Total - expected) <= 0.01m;
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
            .WithMessage("No se pueden pedir mas de 100 unidades por plato");

        RuleFor(x => x.PrecioUnitario)
            .GreaterThan(0)
            .WithMessage("El precio unitario debe ser mayor a 0");
    }
}
