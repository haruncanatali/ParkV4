using FluentValidation;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshToken).NotEmpty()
                .WithName(GlobalPropertyDisplayName.UserRefreshToken);
        }
    }
}