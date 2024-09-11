using FluentValidation;

namespace ParkV4.Application.Entries.Commands.Create;

public class CreateEntryCommandValidator : AbstractValidator<CreateEntryCommand>
{
    public CreateEntryCommandValidator()
    {
        RuleFor(c => c.VehicleId)
            .NotNull().WithMessage("Araç seçilmelidir.");
        RuleFor(c => c.CustomerId)
            .NotNull().WithMessage("Müşteri seçilmelidir.");
        RuleFor(c => c.LocationId)
            .NotNull().WithMessage("Konum seçilmelidir.");
        RuleFor(c => c.FirstDuration)
            .NotNull().WithMessage("Süre girilmelidir.");
    }
}