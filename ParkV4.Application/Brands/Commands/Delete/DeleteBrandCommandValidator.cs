using FluentValidation;

namespace ParkV4.Application.Brands.Commands.Delete;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Marka ID boş olamaz.");
    }
}