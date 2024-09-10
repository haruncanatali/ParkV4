using FluentValidation;

namespace ParkV4.Application.Companies.Commands.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Şirket adı girilmelidir.")
            .MaximumLength(100).WithMessage("Şirket adı en fazla 100 karakterden oluşmak zorundadır.");
    }
}