using FluentValidation;

namespace ParkV4.Application.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakterden oluşabilir.")
            .NotEmpty().WithMessage("Marka adı boş olamaz.");
    }
}