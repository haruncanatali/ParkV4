using FluentValidation;

namespace ParkV4.Application.Entries.Commands.Delete;

public class DeleteEntryCommandValidator : AbstractValidator<DeleteEntryCommand>
{
    public DeleteEntryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Faaliyet ID bulunamadı.");
    }
}