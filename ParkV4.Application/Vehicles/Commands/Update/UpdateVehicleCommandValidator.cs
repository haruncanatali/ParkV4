using FluentValidation;

namespace ParkV4.Application.Vehicles.Commands.Update;

public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Araç ID bulunamadı.");
        RuleFor(c => c.VehicleType)
            .NotNull().WithMessage("Araç tipi seçilidir.");
        RuleFor(c => c.FuelType)
            .NotNull().WithMessage("Yakıt tipi seçilidir.");
        RuleFor(c => c.Plate)
            .NotEmpty().WithMessage("Plaka girilmelidir.")
            .MaximumLength(12).WithMessage("Plaka en fazla 12 haneden oluşabilir.")
            .MinimumLength(7).WithMessage("Plaka en az 7 haneden oluşmak zorundadır.");
        RuleFor(c => c.Color)
            .NotEmpty().WithMessage("Renk girilmelidir.")
            .MaximumLength(75).WithMessage("Renk en fazla 75 karakterden oluşmak zorundadır.");
        RuleFor(c=>c.ModelId)
            .NotNull().WithMessage("Model girilmelidir.");
        RuleFor(c=>c.BrandId)
            .NotNull().WithMessage("Marka girilmelidir.");
    }
}