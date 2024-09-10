using FluentValidation;

namespace ParkV4.Application.Vehicles.Commands.Delete;

public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Araç ID bulunamadı.");
    }
}