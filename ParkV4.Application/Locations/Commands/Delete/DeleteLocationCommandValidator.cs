using FluentValidation;

namespace ParkV4.Application.Locations.Commands.Delete;

public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Konum ID bulunamadı.");
    }
}