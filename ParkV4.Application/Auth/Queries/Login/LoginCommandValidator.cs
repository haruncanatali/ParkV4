using FluentValidation;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithName(GlobalPropertyDisplayName.UserPassword);
            RuleFor(x => x.Username).NotEmpty().WithName(GlobalPropertyDisplayName.UserName);
        }
    }
}