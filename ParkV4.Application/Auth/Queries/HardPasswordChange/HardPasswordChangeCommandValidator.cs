
using FluentValidation;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.HardPasswordChange
{
    public class HardPasswordChangeCommandValidator : AbstractValidator<HardPasswordChangeCommand>
{
    public HardPasswordChangeCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithName(GlobalPropertyDisplayName.UserPassword)
            .MinimumLength(8).WithMessage("Şifre uzunluğunuz en az 8 olmalıdır.")
            .Matches(@"[A-Z]+").WithMessage("Şifreniz en az bir büyük harf içermelidir.")
            .Matches(@"[a-z]+").WithMessage("Şifreniz en az bir küçük harf içermelidir.")
            .Matches(@"[0-9]+").WithMessage("Parolanız en az bir sayı içermelidir.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Şifreniz en az bir tane (!? *.) içermelidir.");
    }
}
}