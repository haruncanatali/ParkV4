using FluentValidation;

namespace ParkV4.Application.Locations.Commands.Update;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Konum ID bulunamadı.");
        RuleFor(c => c.CompanyId)
            .NotNull().WithMessage("Şirket girilmelidir.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Konum Adı girilmelidir.")
            .MaximumLength(50).WithMessage("Konum Adı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Description)
            .MaximumLength(250).WithMessage("Konum Açıklaması en fazla 250 karakterden oluşmak zorundadır.");
    }
}