using FluentValidation;

namespace ParkV4.Application.Models.Commands.Create;

public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
{
    public CreateModelCommandValidator()
    {
        RuleFor(c => c.BrandId)
            .NotNull().WithMessage("Marka girilmesi zorunludur.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Model adı girilmesi zorunludur.")
            .MaximumLength(100).WithMessage("Model adı en fazla 100 karakterden oluşmak zorundadır.");
    }
}