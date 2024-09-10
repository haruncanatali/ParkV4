using FluentValidation;

namespace ParkV4.Application.Locations.Commands.Create;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(c => c.CompanyId)
            .NotNull().WithMessage("Şirket girilmelidir.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Konum Adı girilmelidir.")
            .MaximumLength(50).WithMessage("Konum Adı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Description)
            .MaximumLength(250).WithMessage("Konum Açıklaması en fazla 250 karakterden oluşmak zorundadır.");
    }
}