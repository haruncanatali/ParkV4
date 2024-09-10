using FluentValidation;

namespace ParkV4.Application.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Kullanıcı ID bulunamadı.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Ad alanı zorunludur.")
            .MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Surname)
            .NotEmpty().WithMessage("Soyad alanı zorunludur.")
            .MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("Kullanıcı Adı alanı zorunludur.")
            .MinimumLength(8).WithMessage("Kullanıcı Adı alanı en az 8 karakterden oluşmak zorundadır.")
            .MaximumLength(15).WithMessage("Kullanıcı Adı alanı en fazla 15 karakterden oluşmak zorundadır.");
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("E-Posta alanı zorunludur.")
            .MaximumLength(150).WithMessage("E-Posta alanı en fazla 150 karakterden oluşmak zorundadır.");
        RuleFor(c => c.TelephoneNumber)
            .NotEmpty().WithMessage("Cep telefonu alanı zorunludur.")
            .MaximumLength(20).WithMessage("Cep telefonu alanı en fazla 20 karakterden oluşmak zorundadır.");
        RuleFor(c => c.CompanyId)
            .NotNull().WithMessage("Şirket seçilmelidir.");
    }
}