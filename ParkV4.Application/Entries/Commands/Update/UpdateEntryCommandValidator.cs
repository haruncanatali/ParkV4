using FluentValidation;

namespace ParkV4.Application.Entries.Commands.Update;

public class UpdateEntryCommandValidator : AbstractValidator<UpdateEntryCommand>
{
    public UpdateEntryCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Faaliyet ID bulunamadı.");
        RuleFor(c => c.VehicleId)
            .NotNull().WithMessage("Araç seçilmelidir.");
        RuleFor(c => c.CustomerId)
            .NotNull().WithMessage("Müşteri seçilmelidir.");
        RuleFor(c => c.LocationId)
            .NotNull().WithMessage("Konum seçilmelidir.");
        
    }
}