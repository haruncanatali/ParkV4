using FluentValidation;

namespace ParkV4.Application.Companies.Commands.Delete;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Şirket ID bulunamadı.");
    }
}