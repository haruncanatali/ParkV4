using FluentValidation;

namespace ParkV4.Application.Customers.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.IdentityNumber)
            .NotEmpty().WithMessage("T.C. Kimlik numarası girilmelidir.")
            .MaximumLength(11).WithMessage("T.C. Kimklik numarası 11 haneden oluşmak zorundadır.")
            .MinimumLength(11).WithMessage("T.C. Kimklik numarası 11 haneden oluşmak zorundadır.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Ad girilmelidir.")
            .MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Surname)
            .NotEmpty().WithMessage("Soyad girilmelidir.")
            .MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.TelephoneNumber)
            .NotEmpty().WithMessage("Telefon Numarası girilmelidir.")
            .MaximumLength(20).WithMessage("Telefon numarası alanı en fazla 50 karakterden oluşmak zorundadır.");
    }   
}