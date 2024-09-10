using FluentValidation;

namespace ParkV4.Application.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c=>c.Id)
            .NotNull().WithMessage("Kullanıcı ID bulunamadı.");
    }
}