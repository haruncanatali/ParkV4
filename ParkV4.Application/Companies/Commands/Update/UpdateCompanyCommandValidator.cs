using FluentValidation;

namespace ParkV4.Application.Companies.Commands.Update;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Şirket ID bulunamadı.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Şirket adı girilmelidir.")
            .MaximumLength(100).WithMessage("Şirket adı en fazla 100 karakterden oluşmak zorundadır.");
    }
}