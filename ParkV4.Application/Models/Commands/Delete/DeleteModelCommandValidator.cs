using FluentValidation;

namespace ParkV4.Application.Models.Commands.Delete;

public class DeleteModelCommandValidator : AbstractValidator<DeleteModelCommand>
{
    public DeleteModelCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Model ID bulunamadı.");
    }
}