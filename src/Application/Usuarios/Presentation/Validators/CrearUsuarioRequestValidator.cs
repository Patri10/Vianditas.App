using FluentValidation;
using Vianditas.Application.Usuarios.Presentation.DTOs;

namespace Vianditas.Application.Usuarios.Presentation.Validators;

public class CrearUsuarioRequestValidator : AbstractValidator<CrearUsuarioRequestDTO>
{
    public CrearUsuarioRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio")
            .Length(3, 100)
            .WithMessage("El nombre debe tener entre 3 y 100 caracteres");

        RuleFor(x => x.NumeroWhatsapp)
            .NotEmpty()
            .WithMessage("El numero de WhatsApp es obligatorio")
            .Matches(@"^\+?[1-9]\d{7,14}$")
            .WithMessage("Numero de WhatsApp invalido. Usa formato internacional, por ejemplo +5491122334455")
            .MaximumLength(20)
            .WithMessage("El numero de WhatsApp no puede exceder 20 caracteres");

        RuleFor(x => x.WhatsappUserId)
            .NotEmpty()
            .WithMessage("El identificador de WhatsApp es obligatorio")
            .MaximumLength(64)
            .WithMessage("El identificador de WhatsApp no puede exceder 64 caracteres");
    }
}
