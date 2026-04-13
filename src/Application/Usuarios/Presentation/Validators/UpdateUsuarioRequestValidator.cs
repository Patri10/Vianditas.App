using Vianditas.Application.Usuarios.Presentation.DTOs;
using FluentValidation;

namespace Vianditas.Application.Usuarios.Presentation.Validators;

public class UpdateUsuarioRequestValidator : AbstractValidator<CrearUsuarioRequestDTO>
{
    public UpdateUsuarioRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.NumeroWhatsapp)
            .NotEmpty().WithMessage("El número de WhatsApp es requerido.")
            .Matches(@"^\+\d{1,3}\s?\d{4,14}$").WithMessage("El número de WhatsApp debe tener un formato válido.");

        RuleFor(x => x.WhatsappUserId)
            .NotEmpty().WithMessage("El ID de usuario de WhatsApp es requerido.")
            .MaximumLength(50).WithMessage("El ID de usuario de WhatsApp no puede exceder los 50 caracteres.");
    }
}