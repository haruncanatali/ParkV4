using FluentValidation;

namespace ParkV4.Application.Models.Commands.Update;

public class UpdateModelCommandValidator : AbstractValidator<UpdateModelCommand>
{
    public UpdateModelCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Model ID boş olamaz.");
        RuleFor(c => c.BrandId)
            .NotNull().WithMessage("Marka girilmesi zorunludur.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Model adı girilmesi zorunludur.")
            .MaximumLength(100).WithMessage("Model adı en fazla 100 karakterden oluşmak zorundadır.");
    }
}