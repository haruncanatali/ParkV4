using FluentValidation;

namespace ParkV4.Application.Brands.Commands.Update;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Marka ID boş olamaz.");
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakterden oluşabilir.")
            .NotEmpty().WithMessage("Marka adı boş olamaz.");
    }
}